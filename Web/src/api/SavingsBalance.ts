import { TSavingsBalance, TSavingsBalanceCreate } from "../types/SavingsBalance";

import { z } from "zod";
import { Endpoint } from "../constants/ApiConst";
import { TErrorResponse } from "../types/Api";
import HttpClient from "./HttpClient";

export namespace SavingsBalance {
    export const CreateSavingsBalnce = async (
        httpClient: HttpClient,
        createSavingsBalanceRequest: {
            savingsId: string;
            data: TSavingsBalanceCreate;
        },
        schema: z.ZodType<TSavingsBalance>
    ): Promise<TSavingsBalance | TErrorResponse> => {
        return await httpClient.post<TSavingsBalance, TSavingsBalanceCreate>(
            Endpoint.SavingsBalance.CreateSavingsBalance(createSavingsBalanceRequest.savingsId),
            createSavingsBalanceRequest.data,
            schema
        );
    };

    export const DeleteSavingsBalance = async (
        httpClient: HttpClient,
        deleteSavingsBalanceRequest: { savingsId: string; savingsBalanceId: string }
    ): Promise<void | TErrorResponse> => {
        await httpClient.delete(
            Endpoint.SavingsBalance.DeleteSavingsBalance(deleteSavingsBalanceRequest.savingsId, deleteSavingsBalanceRequest.savingsBalanceId)
        );
    };
}
