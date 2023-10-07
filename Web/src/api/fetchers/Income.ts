import { fetchOptions, handleResponse, url } from '../../utils/ApiUtils';

import { Endpoint } from '../../constants/ApiConst';
import { TIncome } from '../../types/Income';

export const GetIncome = async (accessToken: string): Promise<TIncome[]> => {
    const response = await fetch(
        url(Endpoint.Income),
        fetchOptions({
            method: 'GET',
            accessToken: accessToken,
        })
    );

    return handleResponse(response, 'Json');
};
