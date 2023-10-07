import { fetchOptions, handleResponse, url } from '../../utils/ApiUtils';

import { Endpoint } from '../../constants/ApiConst';
import { TUser } from '../../types/User';

export const GetUser = async (accessToken: string): Promise<TUser> => {
    return handleResponse(
        await fetch(
            url(Endpoint.User),
            fetchOptions({
                method: 'GET',
                accessToken: accessToken,
            })
        ),
        'Json'
    );
};
