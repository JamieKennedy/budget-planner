import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse, isAxiosError } from 'axios';

import { z } from 'zod';
import { TErrorResponse } from '../types/Api';
import { HttpClientUtils } from '../utils/HttpClientUtils';

class HttpClient {
    private _client: AxiosInstance;

    constructor(config: AxiosRequestConfig) {
        this._client = axios.create(config);
    }

    public get = async <T>(url: string, schema?: z.ZodType<T>, config?: AxiosRequestConfig): Promise<T | TErrorResponse> => {
        try {
            console.log(url);
            const response: AxiosResponse = await this._client.get(url, config);
            if (schema) {
                const result = schema.safeParse(response.data);

                if (result.success) {
                    return result.data;
                }

                console.log(result.error);
                throw new Error(result.error.toString());
            }
            return response.data as T;
        } catch (error) {
            return this.handleError(error);
        }
    };

    public post = async <T, V>(url: string, data?: V, schema?: z.ZodType<T>, config?: AxiosRequestConfig): Promise<T | TErrorResponse> => {
        try {
            console.log(url);
            const response: AxiosResponse<T> = await this._client.post(url, data, config);

            if (schema) {
                const result = schema.safeParse(response.data);

                if (result.success) {
                    return result.data;
                }

                console.log(result.error);
                throw new Error(result.error.toString());
            }
            return response.data as T;
        } catch (error) {
            return this.handleError(error);
        }
    };

    public patch = async <T, V>(url: string, data: V, schema?: z.ZodType<T>, config?: AxiosRequestConfig): Promise<T | TErrorResponse> => {
        try {
            console.log(url);
            const response: AxiosResponse<T> = await this._client.patch(url, data, config);
            if (schema) {
                const result = schema.safeParse(response.data);

                if (result.success) {
                    return result.data;
                }

                console.log(result.error);
                throw new Error(result.error.toString());
            }
            return response.data as T;
        } catch (error) {
            return this.handleError(error);
        }
    };

    public delete = async <T>(url: string, schema?: z.ZodType<T>, config?: AxiosRequestConfig): Promise<T | TErrorResponse> => {
        try {
            console.log(url);
            const response: AxiosResponse<T> = await this._client.delete(url, config);
            return response.data;
        } catch (error) {
            return this.handleError(error);
        }
    };

    private handleError = (error: unknown): TErrorResponse => {
        console.log(isAxiosError(error));
        if (isAxiosError(error)) {
            return {
                StatusCode: error.response?.status ?? 500,
                Message: error.response?.data?.Message ?? error.message ?? 'Something went wrong',
            } as TErrorResponse;
        }

        if (HttpClientUtils.IsErrorResponse(error)) {
            return error;
        }

        return {
            StatusCode: 500,
            Message: 'Something went wrong',
        };
    };
}

export default HttpClient;
