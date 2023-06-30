import { useEffect, useState } from "react";

import { Navigate } from "react-router";
import { NavigationConst } from "../../constants/NavigationConst";
import { useClerk } from "@clerk/clerk-react";

enum AuthState {
    Loading,
    Failure,
    Success,
}

const PostSignUp = () => {
    const [authState, setAuthState] = useState(AuthState.Loading);
    const { signOut } = useClerk();

    useEffect(() => {
        // TODO: call api and create user
        setAuthState(AuthState.Success);
    }, [authState]);

    if (authState === AuthState.Loading) {
        return <h1>Loading...{/* TODO: Add Loading Component */}</h1>;
    }

    if (authState === AuthState.Failure) {
        signOut();
    }

    return <Navigate to={NavigationConst.Root} />;
};

export default PostSignUp;
