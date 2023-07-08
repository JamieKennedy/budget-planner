import { useCallback, useEffect, useRef, useState } from "react";
import { Navigate, Outlet } from "react-router";

import { NavigationConst } from "../../constants/NavigationConst";
import useAuth from "../../hooks/useAuth";
import { useLogout } from "../../hooks/useLogout";
import { AuthState } from "../../types/Enum";

const ProtectedRoute = () => {
    const [authState, setAuthState] = useState<AuthState>(AuthState.Success);

    const [getAccessToken] = useAuth();
    const logout = useLogout();

    const refresh = useCallback(async () => {
        const [, error] = await getAccessToken();
        if (error) {
            setAuthState(AuthState.Failure);
            return;
        }

        setAuthState(AuthState.Success);
    }, [getAccessToken]);

    const isMounted = useRef(false);
    useEffect(() => {
        if (isMounted.current) return;

        refresh();

        isMounted.current = true;
    });

    if (authState == AuthState.Pending) {
        return <p className='text-white'>Loading...</p>;
    }

    if (authState == AuthState.Failure) {
        logout();
        return <Navigate to={NavigationConst.Login} />;
    }

    return <Outlet />;
};

export default ProtectedRoute;
