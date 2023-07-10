import { z } from "zod";
import { Endpoint } from "../constants/ApiConst";
import { TErrorResponse } from "../types/Api";
import { TSavings } from "../types/Savings";
import HttpClient from "./HttpClient";

export namespace Savings {
    export const GetSavingsByUserId = async (httpClient: HttpClient, userId: string, schema: z.ZodType<TSavings[]>): Promise<TSavings[] | TErrorResponse> => {
        return await httpClient.get(`${Endpoint.User.GetUserById}/${userId}/${Endpoint.Savings.GetSavingsForUserId}`, schema);
    };
}
