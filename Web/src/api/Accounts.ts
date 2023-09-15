import { TAccount, TCreateAccount, TUpdateAccount } from "../types/Accounts";

import { z } from "zod";
import { Endpoint } from "../constants/ApiConst";
import { TErrorResponse } from "../types/Api";
import HttpClient from "./HttpClient";

export namespace Accounts {
    export const GetAccountsForUser = async (httpClient: HttpClient, userId: string, schema: z.ZodType<TAccount[]>): Promise<TAccount[] | TErrorResponse> => {
        return httpClient.get(Endpoint.Account.Base(userId), schema);
    };

    export const CreateAccount = async (httpClient: HttpClient, request: TCreateAccount): Promise<TAccount | TErrorResponse> => {
        return httpClient.post(Endpoint.Account.Base(request.userId), request);
    };

    export const UpdateAccount = async (httpClient: HttpClient, request: TUpdateAccount): Promise<TAccount | TErrorResponse> => {
        return httpClient.patch(Endpoint.Account.Patch(request.userId, request.accountId), request);
    };

    export const DeleteAccount = async (HttpClient: HttpClient, request: { userId: string; accountId: string }): Promise<void | TErrorResponse> => {
        return HttpClient.delete(`${Endpoint.Account.Base(request.userId)}/${request.accountId}`);
    };
}
