export const Endpoint = {
    Authentication: {
        Login: '/Authentication',
        Refresh: '/Authentication/Refresh',
        Logout: '/Authentication/Logout',
    },
    User: '/User',
    Savings: '/Savings',
    SavingsBalance: (savingsId: string) => `/savings/${savingsId}/SavingsBalance` as const,
    Account: '/Account',
    Income: '/Income',
} as const;
