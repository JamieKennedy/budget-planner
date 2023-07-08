import { useState } from "react";
import HttpClient from "../api/HttpClient";
import { ErrorResponse } from "../types/Api";
import { HttpClientUtils } from "../utils/HttpClientUtils";
import useAuth from "./useAuth";

type useApiResponse<T, U> = [(requestData: U) => Promise<apiResponse<T>>, boolean];
export type apiResponse<T> = [T, undefined] | [undefined, ErrorResponse];
// eslint-disable-next-line @typescript-eslint/no-explicit-any
type requestFn<T> = (httpClient: HttpClient, ...args: any[]) => Promise<T | ErrorResponse>;

/**
 * A generic hook for calling the api, that takes a request function, that must take a HttpClient arg and returns
 * a function to call the provided function with args, the loading state of the call
 * @template T The return type of a successful api call
 * @template U The data to call the api with
 * @param {((httpClient: HttpClient, ...args: any[]) => Promise<T | ErrorResponse>)} fetcher The request function
 * @param requiresAuth bool to specify if the request needs auth, defaults to false
 * @return {*}  {useApiResponse<T, U>}
 */
const useApi = <T, U = void>(fetcher: requestFn<T>, requiresAuth?: boolean, requiresCredentials?: boolean): useApiResponse<T, U> => {
    const [isLoading, setIsLoading] = useState(false);
    const [getAccessToken] = useAuth();

    const getClient = async (): Promise<HttpClient | ErrorResponse> => {
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

    const callApi = async (requestData?: U): Promise<apiResponse<T>> => {
        setIsLoading(true);

        // attempt to create the client with appropriate data
        const [client, clientError] = HttpClientUtils.HandleErrorResponse(await getClient());

        if (clientError) {
            // An error occurred setting up the http client
            setIsLoading(false);
            return [undefined, clientError];
        }

        const data = HttpClientUtils.HandleErrorResponse(await fetcher(client, requestData));

        setIsLoading(false);
        return data;
    };

    return [callApi, isLoading];
};

export default useApi;
