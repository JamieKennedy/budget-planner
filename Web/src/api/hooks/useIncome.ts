import { useQuery } from '@tanstack/react-query';
import { TErrorResponse } from '../../types/Api';
import { TIncome } from '../../types/Income';
import { authenticatedFetcher } from '../../utils/ApiUtils';
import { accessTokenExpired } from '../../utils/JwtUtils';
import { GetIncome } from '../fetchers/Income';
import useAuth from './useAuth';

const useIncome = () => {
    const { accessToken } = useAuth();

    const queryEnabled = accessToken.data !== undefined && !accessTokenExpired(accessToken.data);

    const income = useQuery<TIncome[], TErrorResponse>({
        queryKey: ['getIncome'],
        queryFn: () => authenticatedFetcher(GetIncome, accessToken.data),
        initialData: [],
        enabled: queryEnabled,
    });

    return { income };
};

export default useIncome;
