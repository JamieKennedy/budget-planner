import { StateCreator, create } from 'zustand';
import { IAppState, appState } from './AppState';
import { IAuthState, authState } from './AuthState';
import { IErrorState, errorState } from './ErrorState';

type allStateSlices = IAuthState & IAppState & IErrorState;
export type stateCreator<T> = StateCreator<allStateSlices, [], [], T>;

const useAppStore = create<allStateSlices>()((...a) => ({
    ...authState(...a),
    ...appState(...a),
    ...errorState(...a),
}));

export default useAppStore;
