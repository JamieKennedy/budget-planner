import { useNavigate } from "react-router";
import { Authentication } from "../api/Auth";
import HttpClient from "../api/HttpClient";
import { NavigationConst } from "../constants/NavigationConst";
import useAuth from "./useAuth";

export const useLogout = () => {
    const [, setAccessToken] = useAuth();
    const navigate = useNavigate();

    const logout = async () => {
        const httpClient = new HttpClient({ baseURL: import.meta.env.VITE_API_BASEURL, withCredentials: true });
        await Authentication.Logout(httpClient);

        // set the access token to undefined
        setAccessToken();

        // Navigate to login
        navigate(NavigationConst.Login);
    };

    return logout;
};
