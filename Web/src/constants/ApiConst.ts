export const Endpoint = {
    Authentication: {
        Login: '/api/authentication',
        Refresh: '/api/authentication/refresh',
        Logout: '/api/authentication/logout',
    },
    User: {
        GetUserById: '/api/User',
    },
    Savings: {
        GetSavingsForUserId: 'Savings',
        Delete: (userId: string, savingsId: string) => `/api/user/${userId}/savings/${savingsId}`,
        Create: (userId: string) => `/api/user/${userId}/savings`,
        Edit: (userId: string, savingsId: string) => `/api/user/${userId}/savings/${savingsId}`,
    },
    SavingsBalance: {
        CreateSavingsBalance: (savingsId: string) => `/api/savings/${savingsId}/savingsbalance`,
        DeleteSavingsBalance: (savingsId: string, savingsBalanceId: string) => `/api/savings/${savingsId}/savingsbalance/${savingsBalanceId}`,
    },
    Account: {
        Base: '/api/account',
        Patch: (accountId: string) => `${Endpoint.Account.Base}/${accountId}`,
        Delete: (accountId: string) => `${Endpoint.Account.Base}/${accountId}`,
    },
    Income: {
        Base: '/api/income',
        Delete: (incomeId: string) => `${Endpoint.Income.Base}/${incomeId}`,
    },
};
