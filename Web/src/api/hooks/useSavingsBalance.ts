import { useMutation, useQueryClient } from '@tanstack/react-query';
import { TSavingsBalance, TSavingsBalanceCreate } from '../../types/SavingsBalance';
import { CreateSavingsBalance, DeleteSavingsBalance } from '../fetchers/SavingsBalance';

import useAppStore from '../../state/Store';
import { TErrorResponse } from '../../types/Api';
import { TSavings } from '../../types/Savings';
import { authenticatedFetcher } from '../../utils/ApiUtils';
import useAuth from './useAuth';

const useSavingsBalance = (savingsId: string) => {
    const queryClient = useQueryClient();

    const setError = useAppStore((state) => state.setError);

    const { accessToken } = useAuth();

    const createSavingsBalance = useMutation<TSavingsBalance, TErrorResponse, TSavingsBalanceCreate>({
        mutationKey: ['createSavingsBalance', savingsId],
        mutationFn: (request) => authenticatedFetcher(CreateSavingsBalance, accessToken.data, savingsId, request),
        onSuccess: (data) => {
            queryClient.setQueryData<TSavings[]>(['savings'], (current) => {
                if (current) {
                    return current.map((savings) => {
                        if (savings.savingsId === savingsId) {
                            return { ...savings, savingsBalances: [...savings.savingsBalances, data] };
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

    const deleteSavingsBalance = useMutation<void, TErrorResponse, string>({
        mutationKey: ['deleteSavingsBalance', savingsId],
        mutationFn: (request) => authenticatedFetcher(DeleteSavingsBalance, accessToken.data, savingsId, request),
        onSuccess: (_, request) => {
            queryClient.setQueryData<TSavings[]>(['savings'], (current) => {
                if (current) {
                    return current.map((savings) => {
                        if (savings.savingsId === savingsId) {
                            return { ...savings, savingsBalances: savings.savingsBalances.filter((sb) => sb.savingsBalanceId !== request) };
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

    return { createSavingsBalance: createSavingsBalance, deleteSavingsBalance: deleteSavingsBalance };
};

export default useSavingsBalance;
