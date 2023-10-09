import { SavingsSchema, TSavings, TSavingsCreate, TSavingsEdit } from '../../types/Savings';
import { configOptions, urlWithIds } from '../../utils/ApiUtils';

import { Endpoint } from '../../constants/ApiConst';
import AxiosClient from '../AxiosClient';

export const GetSavings = async (client: AxiosClient, accessToken: string): Promise<TSavings[]> => {
    return client.get(Endpoint.Savings, {
        schema: SavingsSchema.array(),
        config: configOptions({ accessToken: accessToken }),
    });
};

export const CreateSavings = async (client: AxiosClient, accessToken: string, request: TSavingsCreate): Promise<TSavings> => {
    return client.post(Endpoint.Savings, {
        data: request,
        schema: SavingsSchema,
        config: configOptions({ accessToken: accessToken }),
    });
};

export const UpdateSavings = async (client: AxiosClient, accessToken: string, savingsId: string, request: TSavingsEdit): Promise<TSavings> => {
    return client.patch(urlWithIds(Endpoint.Savings, savingsId), {
        data: request,
        schema: SavingsSchema,
        config: configOptions({ accessToken: accessToken }),
    });
};

export const DeleteSavings = async (client: AxiosClient, accessToken: string, savingsId: string): Promise<void> => {
    return client.delete(urlWithIds(Endpoint.Savings, savingsId), {
        config: configOptions({ accessToken: accessToken }),
    });
};
