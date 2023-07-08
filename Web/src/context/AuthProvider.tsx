import { ReactNode, createContext } from "react";

import useSWR from "swr";
import { ErrorResponse } from "../types/Api";

export type TAuthContextProps = {
    token?: string;
    tokenError?: ErrorResponse;
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
    const { data: token, error: tokenError, isLoading: authIsLoading } = useSWR<string, ErrorResponse>("/api/authentication/refresh", refreshAccessToken);

    return <AuthContext.Provider value={{ token, tokenError, authIsLoading }}>{children}</AuthContext.Provider>;
};

export { AuthProvider };
