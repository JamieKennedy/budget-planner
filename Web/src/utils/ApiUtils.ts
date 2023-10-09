import { ErrorResponseSchema, TErrorResponse } from '../types/Api';

import { AxiosRequestConfig } from 'axios';
import AxiosClient from '../api/AxiosClient';
import { accessTokenExpired } from './JwtUtils';

type TFetcher<T> = (client: AxiosClient, accessToken: string, ...args: any[]) => Promise<T>;

/**
 * Type narrowing Function to determine if an object is an error response at runtime
 *
 * @param {any} response
 * @return {boolean}  {response is ErrorResponse}
 */
export const isErrorResponse = (response: unknown): response is TErrorResponse => {
    console.log(response);
    console.log(ErrorResponseSchema.safeParse(response));
    return ErrorResponseSchema.safeParse(response).success;
};

export const authenticatedFetcher = async <T>(fetcher: TFetcher<T>, client: AxiosClient, accessToken?: string, ...args: any[]): Promise<T> => {
    return accessToken !== undefined && !accessTokenExpired(accessToken) ? fetcher(client, accessToken, ...args) : Promise.reject('Invalid access token');
};

export const urlWithIds = (url: string, ...ids: string[]) => {
    const seperatedIds = ids.join('/');
    return `${url}/${seperatedIds}`;
};

export const configOptions = ({ requiresCredentials, accessToken }: { requiresCredentials?: boolean; accessToken?: string }): AxiosRequestConfig => {
    return {
        withCredentials: requiresCredentials,
        headers: {
            Authorization: accessToken ? `Bearer ${accessToken}` : undefined,
        },
    };
};
