import { createBrowserRouter } from "react-router-dom";
import ProtectedRoute from "./components/misc/ProtectedRoute";
import { NavigationConst } from "./constants/NavigationConst";
import Dashboard from "./pages/dashboard/Dashboard";
import Login from "./pages/login/Login";

export const router = createBrowserRouter(
    [
        {
            path: NavigationConst.Root,
            element: <ProtectedRoute />,
            children: [
                {
                    path: NavigationConst.Dashboard,
                    element: <Dashboard />,
                },
            ],
        },
        {
            path: NavigationConst.Login,
            element: <Login />,
        },
    ],
    {
        basename: "/budget-planner/web",
    }
);

export default router;
