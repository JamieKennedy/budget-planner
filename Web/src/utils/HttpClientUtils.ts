import { ErrorResponseSchema, TErrorResponse } from "../types/Api";

import { AxiosRequestConfig } from "axios";
import { apiResponse } from "../hooks/useApi";

export namespace HttpClientUtils {
    export interface IHttpConfigOptions {
        baseUrl: string;
        accessToken?: string;
        requiresCredentials?: boolean;
        timeout?: number;
    }

    export const BuildHttpClientConfig = ({ baseUrl, requiresCredentials, accessToken, timeout }: IHttpConfigOptions) => {
        return {
            baseURL: baseUrl,
            timeout: timeout ? timeout : 300000, // Milliseconds
            withCredentials: requiresCredentials,
            headers: {
                Authorization: accessToken ? `Bearer ${accessToken}` : undefined,
            },
        } as AxiosRequestConfig;
    };

    /**
     * Takes in a response that can either be V or ErrorResponse. If the response is ErrorResponse
     * set the error state and return undefined
     *
     * @template V
     * @param {(V | TErrorResponse)} response
     * @return {*}
     */
    export const HandleErrorResponse = <V>(response: V | TErrorResponse): apiResponse<V> => {
        if (IsErrorResponse(response)) {
            return [undefined, response];
        }

        return [response, undefined];
    };

    /**
     * Type narrowing Function to determine if an object is an error response at runtime
     *
     * @param {any} response
     * @return {boolean}  {response is ErrorResponse}
     */
    export const IsErrorResponse = (response: unknown): response is TErrorResponse => {
        return ErrorResponseSchema.safeParse(response).success;
    };

    export const AppendId = (url: string, ...args: string[]) => {
        return url + "/" + args.join("/");
    };
}
