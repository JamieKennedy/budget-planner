export const Endpoint = {
    Authentication: {
        Login: "/api/authentication",
        Refresh: "/api/authentication/refresh",
        Logout: "/api/authentication/logout",
    },
    User: {
        GetUserById: "/api/User",
    },
    Savings: {
        GetSavingsForUserId: "Savings",
    },
    SavingsBalance: {
        CreateSavingsBalance: (savingsId: string) => `/api/savings/${savingsId}/savingsbalance`,
    },
};
