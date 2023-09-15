import { ETheme } from '../types/Enum';
import { TUser } from '../types/User';
import { stateCreator } from './Store';

export interface IAppState {
    User?: TUser;
    Theme: ETheme;
    setTheme: (theme: ETheme) => void;
    setUser: (user: TUser) => void;
}

export const appState: stateCreator<IAppState> = (set) => ({
    Theme: 'Dark',
    setTheme: (theme) =>
        set((state: IAppState) => ({ ...state, Theme: theme })),
    setUser: (user) => set((state: IAppState) => ({ ...state, User: user })),
});
