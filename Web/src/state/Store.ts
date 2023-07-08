import { StateCreator, create } from "zustand";
import { IAuthState, authState } from "./AuthState";

type allStateSlices = IAuthState;
export type stateCreator<T> = StateCreator<allStateSlices, [], [], T>;

const useAppStore = create<allStateSlices>()((...a) => ({
    ...authState(...a),
}));

export default useAppStore;
