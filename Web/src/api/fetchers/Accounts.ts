import { AccountSchema, TAccount, TCreateAccount, TUpdateAccount } from '../../types/Accounts';
import { configOptions, urlWithIds } from '../../utils/ApiUtils';

import { Endpoint } from '../../constants/ApiConst';
import AxiosClient from '../AxiosClient';

export const GetAccounts = async (client: AxiosClient, accessToken: string): Promise<TAccount[]> => {
    return client.get(Endpoint.Account, {
        schema: AccountSchema.array(),
        config: configOptions({ accessToken: accessToken }),
    });
};

export const CreateAccount = async (client: AxiosClient, accessToken: string, request: TCreateAccount): Promise<TAccount> => {
    return client.post(Endpoint.Account, {
        data: request,
        schema: AccountSchema,
        config: configOptions({ accessToken: accessToken }),
    });
};

export const UpdateAccount = async (client: AxiosClient, accessToken: string, request: TUpdateAccount): Promise<TAccount> => {
    return client.patch(urlWithIds(Endpoint.Account, request.accountId), {
        data: request,
        schema: AccountSchema,
        config: configOptions({ accessToken: accessToken }),
    });
};

export const DeleteAccount = async (client: AxiosClient, accessToken: string, accountId: string): Promise<void> => {
    return client.delete(urlWithIds(Endpoint.Account, accountId), {
        config: configOptions({ accessToken: accessToken }),
    });
};
