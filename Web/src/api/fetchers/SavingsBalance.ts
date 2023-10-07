import { TSavingsBalance, TSavingsBalanceCreate } from '../../types/SavingsBalance';
import { fetchOptions, handleResponse, url, urlWithIds } from '../../utils/ApiUtils';

import { Endpoint } from '../../constants/ApiConst';

export const CreateSavingsBalance = async (accessToken: string, savingsId: string, request: TSavingsBalanceCreate): Promise<TSavingsBalance> => {
    return handleResponse(
        await fetch(
            url(Endpoint.SavingsBalance(savingsId)),
            fetchOptions({
                method: 'POST',
                request: request,
                accessToken: accessToken,
            })
        ),
        'Json'
    );
};

export const DeleteSavingsBalance = async (accessToken: string, savingsId: string, savingsBalanceId: string): Promise<void> => {
    return handleResponse(
        await fetch(
            url(urlWithIds(Endpoint.SavingsBalance(savingsId), savingsBalanceId)),
            fetchOptions({
                method: 'DELETE',
                accessToken: accessToken,
            })
        ),
        'Void'
    );
};
