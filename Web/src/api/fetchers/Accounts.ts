import { TAccount, TCreateAccount, TUpdateAccount } from '../../types/Accounts';
import { fetchOptions, handleResponse, url, urlWithIds } from '../../utils/ApiUtils';

import { Endpoint } from '../../constants/ApiConst';

export const GetAccounts = async (accessToken: string): Promise<TAccount[]> => {
    const response = await fetch(
        url(Endpoint.Account),
        fetchOptions({
            method: 'GET',
            accessToken: accessToken,
        })
    );

    return handleResponse(response, 'Json');
};

export const CreateAccount = async (accessToken: string, request: TCreateAccount): Promise<TAccount> => {
    const response = await fetch(
        url(Endpoint.Account),
        fetchOptions({
            method: 'POST',
            request: request,
            accessToken: accessToken,
        })
    );

    return handleResponse(response, 'Json');
};

export const UpdateAccount = async (accessToken: string, request: TUpdateAccount): Promise<TAccount> => {
    const response = await fetch(
        url(urlWithIds(Endpoint.Account, request.accountId)),
        fetchOptions({
            method: 'PATCH',
            request: request,
            accessToken: accessToken,
        })
    );

    return handleResponse(response, 'Json');
};

export const DeleteAccount = async (accessToken: string, accountId: string): Promise<void> => {
    const response = await fetch(
        url(urlWithIds(Endpoint.Account, accountId)),
        fetchOptions({
            method: 'DELETE',
            accessToken: accessToken,
        })
    );

    return handleResponse(response, 'Void');
};
