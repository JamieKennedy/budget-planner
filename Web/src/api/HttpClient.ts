import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse, isAxiosError } from "axios";

import { ErrorResponse } from "../types/Api";
import { HttpClientUtils } from "../utils/HttpClientUtils";

class HttpClient {
    private _client: AxiosInstance;

    constructor(config: AxiosRequestConfig) {
        this._client = axios.create(config);
    }

    public get = async <T>(url: string, config?: AxiosRequestConfig): Promise<T | ErrorResponse> => {
        try {
            console.log(url);
            const response: AxiosResponse<T> = await this._client.get(url, config);
            return response.data;
        } catch (error) {
            return this.handleError(error);
        }
    };

    public post = async <T, V>(url: string, data?: V, config?: AxiosRequestConfig): Promise<T | ErrorResponse> => {
        try {
            console.log(url);
            const response: AxiosResponse<T> = await this._client.post(url, data, config);
            return response.data;
        } catch (error) {
            return this.handleError(error);
        }
    };

    public patch = async <T, V>(url: string, data: V, config?: AxiosRequestConfig): Promise<T | ErrorResponse> => {
        try {
            console.log(url);
            const response: AxiosResponse<T> = await this._client.patch(url, data, config);
            return response.data;
        } catch (error) {
            return this.handleError(error);
        }
    };

    public delete = async <T>(url: string, config?: AxiosRequestConfig): Promise<T | ErrorResponse> => {
        try {
            console.log(url);
            const response: AxiosResponse<T> = await this._client.delete(url, config);
            return response.data;
        } catch (error) {
            return this.handleError(error);
        }
    };

    private handleError = (error: unknown): ErrorResponse => {
        if (isAxiosError(error)) {
            return {
                StatusCode: error.response?.status ?? 500,
                Message: error.response?.data?.[0]?.Message ?? error.message ?? "Something went wrong",
            } as ErrorResponse;
        }

        if (HttpClientUtils.IsErrorResponse(error)) {
            return error;
        }

        return {
            StatusCode: 500,
            Message: "Something went wrong",
        };
    };
}

export default HttpClient;
