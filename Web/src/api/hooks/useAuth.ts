import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { TAuthorizeRequest, TErrorResponse } from '../../types/Api';
import { Login, Logout, Refresh } from '../fetchers/Auth';

import { useState } from 'react';
import { accessTokenExpired } from '../../utils/JwtUtils';

const useAuth = () => {
    const queryClient = useQueryClient();

    const logout = useMutation<void, TErrorResponse>({
        mutationKey: ['logout'],
        mutationFn: () => Logout(),
        onSettled: () => {
            console.log('logout settled');
            queryClient.removeQueries({ queryKey: ['accessToken'] });
        },
        onError: (error) => {
            console.log(error);
        },
    });

    const currentToken = queryClient.getQueryData<string>(['accessToken']);

    const accessTokenEnabled = currentToken === undefined || (currentToken !== undefined && !accessTokenExpired(currentToken));

    const accessToken = useQuery<string, TErrorResponse>({
        queryKey: ['accessToken'],
        queryFn: Refresh,
        retry: (count, error) => {
            if (error.StatusCode === 404 || error.StatusCode === 401 || error.StatusCode === 403 || error.Message === 'Inactive token') {
                return false;
            }

            return count < 3;
        },
        staleTime: 3600000,
        enabled: accessTokenEnabled,
    });

    const login = useMutation<string, TErrorResponse, TAuthorizeRequest>({
        mutationKey: ['login'],
        mutationFn: (request) => Login(request),
        onSuccess: (data) => {
            console.log('login success');
            queryClient.setQueryData(['accessToken'], data);
        },
    });

    return { accessToken, login, logout };
};

export default useAuth;
