import { TSavings, TSavingsCreate } from "../types/Savings";

import { z } from "zod";
import { Endpoint } from "../constants/ApiConst";
import { TErrorResponse } from "../types/Api";
import HttpClient from "./HttpClient";

export namespace Savings {
    export const GetSavingsByUserId = async (httpClient: HttpClient, userId: string, schema: z.ZodType<TSavings[]>): Promise<TSavings[] | TErrorResponse> => {
        return await httpClient.get(`${Endpoint.User.GetUserById}/${userId}/${Endpoint.Savings.GetSavingsForUserId}`, schema);
    };

    export const Delete = async (httpClient: HttpClient, deleteSavingsRequest: { userId: string; savingsId: string }): Promise<void | TErrorResponse> => {
        return await httpClient.delete(Endpoint.Savings.Delete(deleteSavingsRequest.userId, deleteSavingsRequest.savingsId));
    };

    export const Create = async (
        httpClient: HttpClient,
        createSavingsRequest: TSavingsCreate,
        schema: z.ZodType<TSavings>
    ): Promise<TSavings | TErrorResponse> => {
        return await httpClient.post(Endpoint.Savings.Create(createSavingsRequest.userId), createSavingsRequest, schema);
    };
}
