import { TAuthorizeRequest, TErrorResponse } from '../types/Api';

import { Endpoint } from '../constants/ApiConst';
import HttpClient from './HttpClient';

export namespace Authentication {
    export const Login = async (httpClient: HttpClient, loginRequest: TAuthorizeRequest): Promise<string | TErrorResponse> => {
        return await httpClient.post<string, TAuthorizeRequest>(Endpoint.Authentication.Login, loginRequest);
    };

    export const Refresh = async (httpClient: HttpClient): Promise<string | TErrorResponse> => {
        return await httpClient.post<string, TAuthorizeRequest>(Endpoint.Authentication.Refresh);
    };

    export const Logout = async (HttpClient: HttpClient): Promise<void | TErrorResponse> => {
        return await HttpClient.post<void, null>(Endpoint.Authentication.Logout);
    };
}
