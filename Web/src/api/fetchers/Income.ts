import { IncomeSchema, TCreateIncome, TIncome, TUpdateIncome } from '../../types/Income';
import { configOptions, urlWithIds } from '../../utils/ApiUtils';

import { Endpoint } from '../../constants/ApiConst';
import AxiosClient from '../AxiosClient';

export const GetIncome = async (client: AxiosClient, accessToken: string): Promise<TIncome[]> => {
    return client.get(Endpoint.Income, {
        schema: IncomeSchema.array(),
        config: configOptions({ accessToken: accessToken }),
    });
};

export const CreateIncome = async (client: AxiosClient, accessToken: string, request: TCreateIncome): Promise<TIncome> => {
    return client.post(Endpoint.Income, {
        data: request,
        schema: IncomeSchema,
        config: configOptions({ accessToken: accessToken }),
    });
};

export const UpdateIncome = async (client: AxiosClient, accessToken: string, request: TUpdateIncome): Promise<TIncome> => {
    return client.patch(urlWithIds(Endpoint.Income, request.id), {
        data: request,
        schema: IncomeSchema,
        config: configOptions({ accessToken: accessToken }),
    });
};

export const DeleteIncome = async (client: AxiosClient, accessToken: string, incomeId: string): Promise<void> => {
    return client.delete(urlWithIds(Endpoint.Income, incomeId), {
        config: configOptions({ accessToken: accessToken }),
    });
};
