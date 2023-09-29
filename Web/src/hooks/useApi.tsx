import { useState } from 'react';
import { z } from 'zod';
import HttpClient from '../api/HttpClient';
import { TErrorResponse } from '../types/Api';
import { HttpClientUtils } from '../utils/HttpClientUtils';
import useAuth from './useAuth';

type TUseApiResponse<T, U> = [(callerProps: TRequestProps<T, U>) => Promise<TApiResponse<T>>, boolean];
export type TApiResponse<T> = [T, undefined] | [undefined, TErrorResponse];
// eslint-disable-next-line @typescript-eslint/no-explicit-any
type TRequestFn<T, U = undefined> = (httpClient: HttpClient, props: TRequestProps<T, U>) => Promise<T | TErrorResponse>;
export type TRequestProps<T, U = undefined> = { requestData: U; schema?: z.ZodType<T> };

/**
 * A generic hook for calling the api, that takes a request function, that must take a HttpClient arg and returns
 * a function to call the provided function with args, the loading state of the call
 * @template T The return type of a successful api call
 * @template U The data to call the api with
 * @param {((httpClient: HttpClient, ...args: any[]) => Promise<T | TErrorResponse>)} fetcher The request function
 * @param requiresAuth bool to specify if the request needs auth, defaults to false
 * @return {*}  {useApiResponse<T, U>}
 */
const useApi = <T, U = undefined>(fetcher: Function, requiresAuth?: boolean, requiresCredentials?: boolean): TUseApiResponse<T, U> => {
    const [isLoading, setIsLoading] = useState(false);
    const [getAccessToken] = useAuth();

    const getClient = async (): Promise<HttpClient | TErrorResponse> => {
        let accessToken: string | undefined;

        if (requiresAuth) {
            // fetch / refresh the access token
            const [token, tokenError] = await getAccessToken();

            if (tokenError) {
                return tokenError;
            }

            accessToken = token;
        }

        const clientConfig = HttpClientUtils.BuildHttpClientConfig({
            baseUrl: import.meta.env.VITE_API_BASEURL,
            accessToken: accessToken,
            requiresCredentials: requiresCredentials,
        });
        return new HttpClient(clientConfig);
    };

    const callApi = async (props: TRequestProps<T, U>): Promise<TApiResponse<T>> => {
        setIsLoading(true);

        // attempt to create the client with appropriate data
        const [client, clientError] = HttpClientUtils.HandleErrorResponse(await getClient());

        if (clientError) {
            // An error occurred setting up the http client
            setIsLoading(false);
            return [undefined, clientError];
        }

        const data = HttpClientUtils.HandleErrorResponse(await fetcher(client, props));

        setIsLoading(false);
        return data;
    };

    return [callApi, isLoading];
};

export default useApi;
