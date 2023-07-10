import { ReactNode, createContext } from "react";

import useSWR from "swr";
import { TErrorResponse } from "../types/Api";

export type TAuthContextProps = {
    token?: string;
    tokenError?: TErrorResponse;
    authIsLoading: boolean;
};

const refreshAccessToken = async (url: string): Promise<string> => {
    const response = await fetch(url, {
        method: "POST",
        credentials: "include",
    });

    return response.json();
};

export const AuthContext = createContext<TAuthContextProps | undefined>(undefined);

const AuthProvider = ({ children }: { children: ReactNode }) => {
    const { data: token, error: tokenError, isLoading: authIsLoading } = useSWR<string, TErrorResponse>("/api/authentication/refresh", refreshAccessToken);

    return <AuthContext.Provider value={{ token, tokenError, authIsLoading }}>{children}</AuthContext.Provider>;
};

export { AuthProvider };
