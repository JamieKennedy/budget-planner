import { StateCreator, create } from "zustand";
import { IAppState, appState } from "./AppState";
import { IAuthState, authState } from "./AuthState";

type allStateSlices = IAuthState & IAppState;
export type stateCreator<T> = StateCreator<allStateSlices, [], [], T>;

const useAppStore = create<allStateSlices>()((...a) => ({
    ...authState(...a),
    ...appState(...a),
}));

export default useAppStore;
