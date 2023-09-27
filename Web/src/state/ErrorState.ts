import { StateCreator } from 'zustand';

export interface IErrorState {
    Error?: string;
    setError: (error?: string) => void;
}

export const errorState: StateCreator<IErrorState> = (set) => ({
    setError: (error) => set((state: IErrorState) => ({ ...state, Error: error })),
});
