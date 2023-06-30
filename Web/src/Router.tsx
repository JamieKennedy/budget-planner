import { ClerkProvider, SignIn, SignUp, SignedIn, SignedOut } from "@clerk/clerk-react";
import { Route, Routes, useNavigate } from "react-router-dom";

import Dashboard from "./pages/dashboard/Dashboard";
import Landing from "./pages/landing/Landing";
import { NavigationConst } from "./constants/NavigationConst";
import PostSignUp from "./pages/misc/PostSignUp";

const clerkKey = import.meta.env.VITE_CLERK_PUBLISHABLE_KEY;

const Router = () => {
    const navigate = useNavigate();

    return (
        <ClerkProvider publishableKey={clerkKey} navigate={(to) => navigate(to)}>
            <Routes>
                <Route
                    path={NavigationConst.Root}
                    element={
                        <>
                            <SignedIn>
                                <Dashboard />
                            </SignedIn>
                            <SignedOut>
                                <Landing />
                            </SignedOut>
                        </>
                    }
                />
                <Route path={`{NavigationConst.SignIn}/*`} element={<SignIn routing='path' path={NavigationConst.SignIn} />} />
                <Route path={`{NavigationConst.SignUp/*}`} element={<SignUp routing='path' path='/sign-up' afterSignUpUrl={NavigationConst.PostSignUp} />} />
                <Route path={NavigationConst.PostSignUp} element={<PostSignUp />} />
            </Routes>
        </ClerkProvider>
    );
};

export default Router;
