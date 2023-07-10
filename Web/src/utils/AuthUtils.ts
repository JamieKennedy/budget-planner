import jwt_decode from "jwt-decode";
import { TTokenPayload } from "../types/Api";

export namespace AuthUtils {
    export const getTokenPayload = (token: string): TTokenPayload => {
        return jwt_decode<TTokenPayload>(token);
    };

    export const accessTokenExpired = (accessToken: string): boolean => {
        const payload = getTokenPayload(accessToken);

        return !(Date.now() / 1000 < payload.exp);
    };
}
