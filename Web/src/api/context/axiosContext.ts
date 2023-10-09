import { createContext, useContext } from 'react';

import AxiosClient from '../AxiosClient';

const axiosClient: AxiosClient = new AxiosClient({
    baseURL: import.meta.env.VITE_API_BASEURL,
    headers: {
        'Content-Type': 'application/json',
    },
});

const AxiosContext = createContext(axiosClient);

export const useAxiosClient = () => {
    const client = useContext(AxiosContext);

    if (!client) {
        throw new Error('useMyInstance must be used within a MyContextProvider');
    }
    return client;
};
