import { stateCreator } from './Store';

export interface IAuthState {
    Auth?: {
        AccessToken?: string;
    };
    setToken: (accessToken?: string) => void;
}

export const authState: stateCreator<IAuthState> = (set) => ({
    setToken: (accessToken) => set((state: IAuthState) => ({ ...state, Auth: { ...state.Auth, AccessToken: accessToken } })),
});
