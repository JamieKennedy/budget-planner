import { fetchOptions, handleResponse, url } from '../../utils/ApiUtils';

import { Endpoint } from '../../constants/ApiConst';
import { TAuthorizeRequest } from '../../types/Api';

export const Login = async (request: TAuthorizeRequest): Promise<string> => {
    const response = await fetch(
        url(Endpoint.Authentication.Login),
        fetchOptions({
            method: 'POST',
            requiresCredentials: true,
            request: request,
        })
    );

    return handleResponse(response, 'Text');
};

export const Refresh = async (): Promise<string> => {
    console.log('refresh');
    const response = await fetch(
        url(Endpoint.Authentication.Refresh),
        fetchOptions({
            method: 'POST',
            requiresCredentials: true,
        })
    );

    return handleResponse(response, 'Text');
};

export const Logout = async (): Promise<void> => {
    fetch(
        url(Endpoint.Authentication.Logout),
        fetchOptions({
            method: 'POST',
            requiresCredentials: true,
        })
    );
};
