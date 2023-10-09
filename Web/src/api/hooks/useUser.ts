import { useQuery } from '@tanstack/react-query';
import { TErrorResponse } from '../../types/Api';
import { TUser } from '../../types/User';
import { authenticatedFetcher } from '../../utils/ApiUtils';
import { accessTokenExpired } from '../../utils/JwtUtils';
import { useAxiosClient } from '../context/axiosContext';
import { GetUser } from '../fetchers/User';
import useAuth from './useAuth';

const useUser = () => {
    const { accessToken } = useAuth();
    const axiosClient = useAxiosClient();

    const queryEnabled = accessToken.data !== undefined && !accessTokenExpired(accessToken.data);

    const user = useQuery<TUser, TErrorResponse>({
        queryKey: ['user'],
        queryFn: () => authenticatedFetcher(GetUser, axiosClient, accessToken.data),
        enabled: queryEnabled,
    });

    return { user: user };
};

export default useUser;
