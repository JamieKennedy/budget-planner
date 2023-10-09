import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { TCreateIncome, TIncome, TUpdateIncome } from '../../types/Income';
import { CreateIncome, DeleteIncome, GetIncome, UpdateIncome } from '../fetchers/Income';

import { TErrorResponse } from '../../types/Api';
import { authenticatedFetcher } from '../../utils/ApiUtils';
import { accessTokenExpired } from '../../utils/JwtUtils';
import { useAxiosClient } from '../context/axiosContext';
import useAuth from './useAuth';

const useIncome = () => {
    const queryClient = useQueryClient();

    const { accessToken } = useAuth();
    const axiosClient = useAxiosClient();

    const queryEnabled = accessToken.data !== undefined && !accessTokenExpired(accessToken.data);

    const income = useQuery<TIncome[], TErrorResponse>({
        queryKey: ['income'],
        queryFn: () => authenticatedFetcher(GetIncome, axiosClient, accessToken.data),
        initialData: [],
        enabled: queryEnabled,
    });

    const createIncome = useMutation<TIncome, TErrorResponse, TCreateIncome>({
        mutationKey: ['createIncome'],
        mutationFn: (request) => authenticatedFetcher(CreateIncome, axiosClient, accessToken.data, request),
        onSuccess: (data) => {
            queryClient.setQueryData<TIncome[]>(['income'], (current) => {
                if (current) {
                    return [...current, data];
                }

                return [data];
            });
        },
    });

    const updateIncome = useMutation<TIncome, TErrorResponse, TUpdateIncome>({
        mutationKey: ['updateIncome'],
        mutationFn: (request) => authenticatedFetcher(UpdateIncome, axiosClient, accessToken.data, request),
        onSuccess: (data) => {
            queryClient.setQueryData<TIncome[]>(['income'], (current) => {
                if (current) {
                    return current.map((income) => {
                        if (income.id === data.id) {
                            return data;
                        }

                        return income;
                    });
                }
            });
        },
    });

    const deleteIncome = useMutation<void, TErrorResponse, string>({
        mutationKey: ['deleteIncome'],
        mutationFn: (request) => authenticatedFetcher(DeleteIncome, axiosClient, accessToken.data, request),
        onSuccess: (_, request) => {
            queryClient.setQueryData<TIncome[]>(['income'], (current) => {
                if (current) {
                    return current.filter((income) => income.id !== request);
                }
            });
        },
    });

    return { income, createIncome, updateIncome, deleteIncome };
};

export default useIncome;
