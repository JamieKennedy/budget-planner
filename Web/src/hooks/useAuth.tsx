import { Authentication } from "../api/Auth";
import HttpClient from "../api/HttpClient";
import useAppStore from "../state/Store";
import { AuthUtils } from "../utils/AuthUtils";
import { HttpClientUtils } from "../utils/HttpClientUtils";
import { apiResponse } from "./useApi";

const useAuth = (): [() => Promise<apiResponse<string>>, (accessToken?: string) => void] => {
    const accessToken = useAppStore((appState) => appState.Auth?.AccessToken);
    const setToken = useAppStore((appState) => appState.setToken);

    const getAccessToken = async (): Promise<apiResponse<string>> => {
        if (!accessToken || AuthUtils.accessTokenExpired(accessToken)) {
            // No token in the state or access token in state has expired
            const httpClient = new HttpClient(
                HttpClientUtils.BuildHttpClientConfig({
                    baseUrl: import.meta.env.VITE_API_BASEURL,
                    requiresCredentials: true,
                })
            );

            const [accessToken, tokenError] = HttpClientUtils.HandleErrorResponse(await Authentication.Refresh(httpClient));

            if (tokenError) {
                // return the error
                return [undefined, tokenError];
            }

            // set the access token in the appState and return it
            setToken(accessToken);
            return [accessToken, undefined];
        }

        // Valid token is in state, so return it
        return [accessToken, undefined];
    };

    return [getAccessToken, setToken];
};

export default useAuth;
