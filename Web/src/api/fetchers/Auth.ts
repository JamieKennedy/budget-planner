import { Endpoint } from '../../constants/ApiConst';
import { TAuthorizeRequest } from '../../types/Api';
import { configOptions } from '../../utils/ApiUtils';
import AxiosClient from '../AxiosClient';

export const Login = async (client: AxiosClient, request: TAuthorizeRequest): Promise<string> => {
    return client.post(Endpoint.Authentication.Login, {
        data: request,
        config: configOptions({ requiresCredentials: true }),
    });
};

export const Refresh = async (client: AxiosClient): Promise<string> => {
    console.log('refresh');

    return client.post(Endpoint.Authentication.Refresh, {
        config: configOptions({ requiresCredentials: true }),
    });
};

export const Logout = async (client: AxiosClient): Promise<void> => {
    return client.post(Endpoint.Authentication.Logout, {
        config: configOptions({ requiresCredentials: true }),
    });
};
