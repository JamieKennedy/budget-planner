import { AuthorizeRequest, ErrorResponse } from "../types/Api";

import { Endpoint } from "../constants/ApiConst";
import HttpClient from "./HttpClient";

export namespace Authentication {
    export const Login = async (httpClient: HttpClient, loginRequest: AuthorizeRequest): Promise<string | ErrorResponse> => {
        return await httpClient.post<string, AuthorizeRequest>(Endpoint.Authentication.Login, loginRequest);
    };

    export const Refresh = async (httpClient: HttpClient): Promise<string | ErrorResponse> => {
        return await httpClient.post<string, AuthorizeRequest>(Endpoint.Authentication.Refresh);
    };

    export const Logout = async (HttpClient: HttpClient): Promise<void | ErrorResponse> => {
        return await HttpClient.post<void, null>(Endpoint.Authentication.Logout);
    };
}
