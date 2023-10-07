import { ERequestMethod, EResponseType, ErrorResponseSchema, TErrorResponse } from '../types/Api';

import { z } from 'zod';
import { accessTokenExpired } from './JwtUtils';

type TFetcher<T> = (...args: any[]) => Promise<T>;

type TFetchOptions<T> = {
    method: ERequestMethod;
    request?: T;
    accessToken?: string;
    requiresCredentials?: boolean;
};

export const authenticatedFetcher = async <T>(fetcher: TFetcher<T>, accessToken?: string, ...args: any[]): Promise<T> => {
    return accessToken !== undefined && !accessTokenExpired(accessToken) ? fetcher(accessToken, ...args) : Promise.reject('Invalid access token');
};

export const authHeader = (accessToken: string): string => `Bearer ${accessToken}`;

export const urlWithIds = (url: string, ...ids: string[]) => {
    const seperatedIds = ids.join('/');
    return `${url}/${seperatedIds}`;
};

export const url = (endpoint: string) => {
    return import.meta.env.VITE_API_BASEURL + endpoint;
};

export const fetchOptions = <T = void>(options: TFetchOptions<T>): RequestInit => {
    return {
        method: options.method,
        body: options.request ? JSON.stringify(options.request) : undefined,
        credentials: options.requiresCredentials ? 'include' : undefined,
        headers: {
            'content-type': 'application/json',
            Authorization: options.accessToken ? authHeader(options.accessToken) : '',
        },
    };
};

export const handleResponse = async <T>(response: Response, responseType: EResponseType, schema?: z.ZodType<T>) => {
    if (!response.ok) {
        var error = await response.json();

        const parseResult = ErrorResponseSchema.safeParse(error);

        if (parseResult.success) {
            throw error;
        }

        throw {
            Message: 'An error has occured',
            StatusCode: 500,
        } as TErrorResponse;
    }

    let data = '';

    if (responseType === 'Json') {
        data = await response.json();
    } else if (responseType === 'Text') {
        data = await response.text();
    } else {
        return data as T;
    }

    if (schema) {
        const parseResult = schema.safeParse(data);
        if (parseResult.success) {
            return parseResult.data;
        } else {
            throw {
                StatusCode: 500,
                Message: parseResult.error.message,
            } as TErrorResponse;
        }
    }

    return data as T;
};
