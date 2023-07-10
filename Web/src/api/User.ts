import { Endpoint } from "../constants/ApiConst";
import { TErrorResponse } from "../types/Api";
import { TUser } from "../types/User";
import HttpClient from "./HttpClient";

export namespace User {
    export const GetUserById = async (httpClient: HttpClient, userId: string): Promise<TUser | TErrorResponse> => {
        return await httpClient.get(`${Endpoint.User.GetUserById}/${userId}`);
    };
}
