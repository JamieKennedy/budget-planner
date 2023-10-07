import { SavingsSchema, TSavings, TSavingsCreate, TSavingsEdit } from '../../types/Savings';
import { fetchOptions, handleResponse, url, urlWithIds } from '../../utils/ApiUtils';

import { Endpoint } from '../../constants/ApiConst';

export const GetSavings = async (accessToken: string): Promise<TSavings[]> => {
    const response = await fetch(
        url(Endpoint.Savings),
        fetchOptions({
            method: 'GET',
            accessToken: accessToken,
        })
    );

    return handleResponse(response, 'Json', SavingsSchema.array());
};

export const CreateSavings = async (accessToken: string, request: TSavingsCreate): Promise<TSavings> => {
    console.log(request);
    return handleResponse(
        await fetch(
            url(Endpoint.Savings),
            fetchOptions({
                method: 'POST',
                request: request,
                accessToken: accessToken,
            })
        ),
        'Json'
    );
};

export const UpdateSavings = async (accessToken: string, request: TSavingsEdit): Promise<TSavings> => {
    return handleResponse(
        await fetch(
            url(urlWithIds(Endpoint.Savings, request.savingsId)),
            fetchOptions({
                method: 'PATCH',
                request: request,
                accessToken: accessToken,
            })
        ),
        'Json'
    );
};

export const DeleteSavings = async (accessToken: string, request: string): Promise<void> => {
    return handleResponse(
        await fetch(
            url(urlWithIds(Endpoint.Savings, request)),
            fetchOptions({
                method: 'DELETE',
                accessToken: accessToken,
            })
        ),
        'Void'
    );
};
