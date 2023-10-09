import { Navigate, Outlet, useOutlet } from 'react-router';

import useAuth from '../../api/hooks/useAuth';
import { NavigationConst } from '../../constants/NavigationConst';

const ProtectedRoute = () => {
    const { accessToken } = useAuth();

    const outlet = useOutlet();

    if (accessToken.data === undefined) {
        return <Navigate to={NavigationConst.Login} />;
    }

    if (outlet) {
        return <Outlet />;
    }

    return <Navigate to={NavigationConst.Dashboard} />;
};

export default ProtectedRoute;
