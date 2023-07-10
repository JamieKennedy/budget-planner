import { useCallback, useEffect, useRef, useState } from "react";
import { Navigate, Outlet, useOutlet } from "react-router";

import { User } from "../../api/User";
import { NavigationConst } from "../../constants/NavigationConst";
import useApi from "../../hooks/useApi";
import useAuth from "../../hooks/useAuth";
import { useLogout } from "../../hooks/useLogout";
import useAppStore from "../../state/Store";
import { AuthState } from "../../types/Enum";
import { TUser } from "../../types/User";
import { AuthUtils } from "../../utils/AuthUtils";

const ProtectedRoute = () => {
    const [authState, setAuthState] = useState<AuthState>(AuthState.Pending);
    const [user, setUser] = useAppStore((appState) => [appState.User, appState.setUser]);
    const [getAccessToken] = useAuth();
    const [getUser] = useApi<TUser, string>(User.GetUserById, true);
    const logout = useLogout();
    const outlet = useOutlet();

    const refresh = useCallback(async () => {
        const [accessToken, error] = await getAccessToken();
        if (error) {
            setAuthState(AuthState.Failure);
            return;
        }
        if (user) {
            setAuthState(AuthState.Success);
        }

        const userId = AuthUtils.getTokenPayload(accessToken).Id;

        const [userData, userError] = await getUser(userId);

        if (userError) {
            setAuthState(AuthState.Failure);
            return;
        }

        setUser(userData);
        setAuthState(AuthState.Success);
    }, [getAccessToken, getUser, setUser, user]);

    const isMounted = useRef(false);
    useEffect(() => {
        if (isMounted.current) return;

        refresh();

        isMounted.current = true;
    }, [refresh]);

    if (authState == AuthState.Pending) {
        return <p className='text-white'>Loading...</p>;
    }

    if (authState == AuthState.Failure) {
        logout();
        return <Navigate to={NavigationConst.Login} />;
    }

    if (outlet) {
        return <Outlet />;
    }

    return <Navigate to={NavigationConst.Dashboard} />;
};

export default ProtectedRoute;