import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { TSavings, TSavingsCreate, TSavingsEdit } from '../../types/Savings';
import { CreateSavings, DeleteSavings, GetSavings, UpdateSavings } from '../fetchers/Savings';

import useAppStore from '../../state/Store';
import { TErrorResponse } from '../../types/Api';
import { authenticatedFetcher } from '../../utils/ApiUtils';
import { accessTokenExpired } from '../../utils/JwtUtils';
import useAuth from './useAuth';

const useSavings = () => {
    const queryClient = useQueryClient();
    const setError = useAppStore((state) => state.setError);

    const { accessToken } = useAuth();

    const queryEnabled = accessToken.data !== undefined && !accessTokenExpired(accessToken.data);

    const savings = useQuery<TSavings[], TErrorResponse>({
        queryKey: ['savings'],
        queryFn: () => authenticatedFetcher(GetSavings, accessToken.data),
        enabled: queryEnabled,
        initialData: [],
    });

    const createSavings = useMutation<TSavings, TErrorResponse, TSavingsCreate>({
        mutationKey: ['createSavings'],
        mutationFn: (request) => authenticatedFetcher(CreateSavings, accessToken.data, request),
        onSuccess: (data) => {
            queryClient.setQueryData<TSavings[]>(['savings'], (current) => {
                if (current) {
                    return [...current, data];
                }

                return [data];
            });
        },
        onError(error) {
            setError(error.Message);
        },
    });

    const updateSavings = useMutation<TSavings, TErrorResponse, TSavingsEdit>({
        mutationKey: ['updateSavings'],
        mutationFn: (request) => authenticatedFetcher(UpdateSavings, accessToken.data, request),
        onSuccess: (data, request) => {
            queryClient.setQueryData<TSavings[]>(['savings'], (current) => {
                if (current) {
                    return current.map((savings) => {
                        if (savings.savingsId === request.savingsId) {
                            return data;
                        }

                        return savings;
                    });
                }
            });
        },
        onError: (error) => {
            setError(error.Message);
        },
    });

    const deleteSavings = useMutation<void, TErrorResponse, string>({
        mutationKey: ['deleteSavings'],
        mutationFn: (request) => authenticatedFetcher(DeleteSavings, accessToken.data, request),
        onSuccess: (_, request) => {
            queryClient.setQueryData<TSavings[]>(['savings'], (current) => {
                if (current) {
                    return current.filter((savings) => savings.savingsId !== request);
                }
            });
        },
        onError: (error) => {
            setError(error.Message);
        },
    });

    return { savings: savings, createSavings: createSavings, updateSavings: updateSavings, deleteSavings: deleteSavings };
};

export default useSavings;
