import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse, isAxiosError } from 'axios';

import { z } from 'zod';
import { TErrorResponse } from '../types/Api';
import { isErrorResponse } from '../utils/ApiUtils';

type TQueryOptions<T, V = undefined> = {
    data?: V;
    schema?: z.ZodType<T>;
    config?: AxiosRequestConfig;
};

class AxiosClient {
    private _client: AxiosInstance;

    constructor(config: AxiosRequestConfig) {
        this._client = axios.create(config);
    }

    public get = async <T>(url: string, options: TQueryOptions<T> = {}) => {
        try {
            const response = await this._client.get<T>(url, options.config);

            if (options.schema) {
                return this.parseResult(response, options.schema);
            }

            return response.data;
        } catch (e) {
            throw this.handleError(e);
        }
    };

    public post = async <T, V>(url: string, options: TQueryOptions<T, V>) => {
        try {
            const response = await this._client.post<T>(url, options.data, options.config);

            if (options.schema) {
                return this.parseResult(response, options.schema);
            }

            return response.data;
        } catch (e) {
            throw this.handleError(e);
        }
    };

    public patch = async <T, V>(url: string, options: TQueryOptions<T, V>) => {
        try {
            const response = await this._client.patch<T>(url, options.data, options.config);

            if (options.schema) {
                return this.parseResult(response, options.schema);
            }

            return response.data;
        } catch (e) {
            throw this.handleError(e);
        }
    };

    public delete = async <T = void>(url: string, options: TQueryOptions<T>) => {
        try {
            const response = await this._client.delete<T>(url, options.config);

            if (options.schema) {
                return this.parseResult(response, options.schema);
            }

            return response.data;
        } catch (e) {
            throw this.handleError(e);
        }
    };

    private parseResult = <T>(response: AxiosResponse, schema: z.ZodType<T>): T => {
        const parseResult = schema.safeParse(response.data);

        if (parseResult.success) {
            return parseResult.data;
        }

        console.log(parseResult.error);
        throw {
            StatusCode: 500,
            Message: parseResult.error.message,
        } as TErrorResponse;
    };

    private handleError = (error: unknown) => {
        console.log(isAxiosError(error));
        if (isAxiosError(error)) {
            return {
                StatusCode: error.response?.status ?? 500,
                Message: error.response?.data?.Message ?? error.message ?? 'Something went wrong',
            } as TErrorResponse;
        }

        if (isErrorResponse(error)) {
            return error;
        }

        return {
            StatusCode: 500,
            Message: 'Something went wrong',
        } as TErrorResponse;
    };
}

export default AxiosClient;
