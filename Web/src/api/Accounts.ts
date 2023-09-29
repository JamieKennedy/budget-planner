import { TAccount, TCreateAccount, TUpdateAccount } from '../types/Accounts';

import { Endpoint } from '../constants/ApiConst';
import { TRequestProps } from '../hooks/useApi';
import { TErrorResponse } from '../types/Api';
import HttpClient from './HttpClient';

export namespace Accounts {
    export const GetAccountsForUser = async (httpClient: HttpClient, { schema }: TRequestProps<TAccount[]>): Promise<TAccount[] | TErrorResponse> => {
        return httpClient.get(Endpoint.Account.Base, schema);
    };

    export const CreateAccount = async (httpClient: HttpClient, { requestData, schema }: TRequestProps<TAccount, TCreateAccount>): Promise<TAccount | TErrorResponse> => {
        return httpClient.post(Endpoint.Account.Base, requestData, schema);
    };

    export const UpdateAccount = async (httpClient: HttpClient, { requestData, schema }: TRequestProps<TAccount, TUpdateAccount>): Promise<TAccount | TErrorResponse> => {
        return httpClient.patch(Endpoint.Account.Patch(requestData.accountId), requestData, schema);
    };

    export const DeleteAccount = async (HttpClient: HttpClient, { requestData, schema }: TRequestProps<void, string>): Promise<void | TErrorResponse> => {
        return HttpClient.delete(Endpoint.Account.Delete(requestData), schema);
    };
}
