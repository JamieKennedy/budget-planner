import { TUser } from "../types/User";
import { stateCreator } from "./Store";

export interface IAppState {
    User?: TUser;
    setUser: (user: TUser) => void;
}

export const appState: stateCreator<IAppState> = (set) => ({
    setUser: (user) => set((state: IAppState) => ({ ...state, User: user })),
});
