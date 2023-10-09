import { TUser, UserSchema } from '../../types/User';

import { Endpoint } from '../../constants/ApiConst';
import { configOptions } from '../../utils/ApiUtils';
import AxiosClient from '../AxiosClient';

export const GetUser = async (client: AxiosClient, accessToken: string): Promise<TUser> => {
    return client.get(Endpoint.User, {
        schema: UserSchema,
        config: configOptions({ accessToken: accessToken }),
    });
};
