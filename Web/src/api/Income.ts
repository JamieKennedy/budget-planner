import { TCreateIncome, TIncome } from '../types/Income';

import { Endpoint } from '../constants/ApiConst';
import { TUpdateAccount } from '../types/Accounts';
import { TErrorResponse } from '../types/Api';
import HttpClient from './HttpClient';

export namespace Income {
    export const GetIncomeForUser = async (httpClient: HttpClient): Promise<TIncome[] | TErrorResponse> => {
        return httpClient.get(Endpoint.Income.Base);
    };

    export const CreateIncome = async (httpClient: HttpClient, request: TCreateIncome): Promise<TIncome | TErrorResponse> => {
        return httpClient.post(Endpoint.Income.Base, request);
    };

    export const UpdateIncome = async (httpClient: HttpClient, request: TUpdateAccount): Promise<TIncome | TErrorResponse> => {
        return httpClient.patch(Endpoint.Income.Base, request);
    };

    export const DeleteIncome = async (httpClient: HttpClient, incomeId: string): Promise<void | TErrorResponse> => {
        return httpClient.delete(Endpoint.Income.Delete(incomeId));
    };
}
