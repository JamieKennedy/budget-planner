import { SavingsBalanceSchema, TSavingsBalance, TSavingsBalanceCreate } from '../../types/SavingsBalance';
import { configOptions, urlWithIds } from '../../utils/ApiUtils';

import { Endpoint } from '../../constants/ApiConst';
import AxiosClient from '../AxiosClient';

export const CreateSavingsBalance = async (client: AxiosClient, accessToken: string, savingsId: string, request: TSavingsBalanceCreate): Promise<TSavingsBalance> => {
    return client.post(Endpoint.SavingsBalance(savingsId), {
        data: request,
        schema: SavingsBalanceSchema,
        config: configOptions({ accessToken: accessToken }),
    });
};

export const DeleteSavingsBalance = async (client: AxiosClient, accessToken: string, savingsId: string, savingsBalanceId: string): Promise<void> => {
    return client.delete(urlWithIds(Endpoint.SavingsBalance(savingsId), savingsBalanceId), {
        config: configOptions({ accessToken: accessToken }),
    });
};
