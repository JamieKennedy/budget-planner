import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { TAccount, TCreateAccount, TUpdateAccount } from '../../types/Accounts';
import { CreateAccount, DeleteAccount, GetAccounts, UpdateAccount } from '../fetchers/Accounts';

import { TErrorResponse } from '../../types/Api';
import { authenticatedFetcher } from '../../utils/ApiUtils';
import { accessTokenExpired } from '../../utils/JwtUtils';
import { useAxiosClient } from '../context/axiosContext';
import useAuth from './useAuth';

const useAccount = () => {
    const queryClient = useQueryClient();

    const axiosClient = useAxiosClient();
    const { accessToken } = useAuth();

    const queryEnabled = accessToken.data !== undefined && !accessTokenExpired(accessToken.data);

    const accountQuery = useQuery<TAccount[], TErrorResponse>({
        queryKey: ['accounts'],
        queryFn: () => authenticatedFetcher(GetAccounts, axiosClient, accessToken.data),
        enabled: queryEnabled,
        initialData: [],
    });

    const createAccount = useMutation<TAccount, TErrorResponse, TCreateAccount>({
        mutationKey: ['createAccount'],
        mutationFn: (request) => authenticatedFetcher(CreateAccount, axiosClient, accessToken.data, request),
        onSuccess: (data) => {
            queryClient.setQueryData<TAccount[]>(['accounts'], (current) => {
                if (current) {
                    return [...current, data];
                }

                return [data];
            });
        },
    });

    const updateAccount = useMutation<TAccount, TErrorResponse, TUpdateAccount>({
        mutationKey: ['updateAccount'],
        mutationFn: (request) => authenticatedFetcher(UpdateAccount, axiosClient, accessToken.data, request),
        onSuccess: (data) => {
            queryClient.setQueryData<TAccount[]>(['accounts'], (current) => {
                if (current) {
                    return current.map((account) => {
                        if (account.id === data.id) {
                            return data;
                        }

                        return account;
                    });
                }
            });
        },
    });

    const deleteAccount = useMutation<void, TErrorResponse, string>({
        mutationKey: ['deleteAccount'],
        mutationFn: (request) => authenticatedFetcher(DeleteAccount, axiosClient, accessToken.data, request),
        onSuccess: (_, request) => {
            console.log('Delete', request);
            queryClient.setQueryData<TAccount[]>(['accounts'], (current) => {
                if (current) {
                    return current.filter((account) => account.id !== request);
                }
            });
        },
        onError(error) {
            console.log(error);
        },
    });

    return { accounts: accountQuery, createAccount: createAccount, updateAccount: updateAccount, deleteAccount: deleteAccount };
};

export default useAccount;
