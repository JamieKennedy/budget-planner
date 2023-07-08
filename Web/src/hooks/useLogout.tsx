import { Authentication } from "../api/Auth";
import HttpClient from "../api/HttpClient";
import useAuth from "./useAuth";

export const useLogout = () => {
    const [, setAccessToken] = useAuth();

    const logout = async () => {
        const httpClient = new HttpClient({ baseURL: "", withCredentials: true });
        await Authentication.Logout(httpClient);

        // set the access token to undefined
        setAccessToken();
    };

    return logout;
};
