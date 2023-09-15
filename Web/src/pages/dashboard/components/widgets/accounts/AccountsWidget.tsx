import { useCallback, useEffect, useRef, useState } from "react";

import { PlusIcon } from "@heroicons/react/20/solid";
import { Accounts } from "../../../../../api/Accounts";
import Modal from "../../../../../components/misc/ui/pageElements/Modal";
import { EWidgetState } from "../../../../../constants/Enum";
import useApi from "../../../../../hooks/useApi";
import useAppStore from "../../../../../state/Store";
import { TAccount } from "../../../../../types/Accounts";
import AccountList from "./components/AccountList";
import AddEditAccount from "./components/AddEditAccount";

const AccountsWidget = () => {
    const [user, setError] = useAppStore((appState) => [appState.User, appState.setError]);

    const [addingAccount, setAddingAccount] = useState(false);
    const [accounts, setAccounts] = useState<TAccount[]>([]);
    const [widgetState, setWidgetState] = useState<EWidgetState>("Loading");

    const [fetchAccounts] = useApi<TAccount[], string>(Accounts.GetAccountsForUser, true);

    const getAccounts = useCallback(async () => {
        if (user) {
            const [remoteAccounts, remoteAccountsError] = await fetchAccounts(user.id);

            if (remoteAccountsError) {
                setError(remoteAccounts);
                setWidgetState("Errored");
                return;
            }

            setWidgetState("Loaded");
            setAccounts(remoteAccounts);
        }
    }, [user, fetchAccounts, setError]);

    const isMounted = useRef(false);
    useEffect(() => {
        if (isMounted.current) return;

        getAccounts();

        isMounted.current = true;

        return () => {
            isMounted.current = true;
        };
    }, [fetchAccounts, getAccounts, user]);

    const removeAccount = (account: TAccount) => {
        setAccounts((current) => {
            return current.filter((c) => c.id !== account.id);
        });
    };

    const addAccount = (account: TAccount) => {
        setAccounts((current) => [...current, account]);
    };

    const editAccount = (account: TAccount) => {
        setAccounts((current) =>
            current.map((c) => {
                if (c.id === account.id) {
                    return account;
                }

                return c;
            })
        );
    };

    return (
        <>
            {user && addingAccount && (
                <Modal closeFn={() => setAddingAccount(false)}>
                    <AddEditAccount addAccount={addAccount} closeFn={() => setAddingAccount(false)} />
                </Modal>
            )}
            <section className='h-fit w-full p-5'>
                <div className='relative'>
                    <div className='absolute inset-0 flex items-center' aria-hidden='true'>
                        <div className='w-full border-t border-gray-300' />
                    </div>
                    <div className='relative flex items-center justify-between'>
                        <span className='bg-white pr-3 text-xl font-semibold text-gray-900'>Accounts</span>
                        <button
                            type='button'
                            className='inline-flex items-center gap-x-1.5 rounded-full bg-white px-3 py-1.5 text-sm font-semibold text-gray-900 shadow-md ring-1 ring-inset ring-gray-300 hover:bg-gray-50'
                            onClick={() => setAddingAccount(true)}
                        >
                            <PlusIcon className='-ml-1 -mr-0.5 h-5 w-5 text-gray-400' aria-hidden='true' />
                            <span>Add new account</span>
                        </button>
                    </div>
                </div>
                <AccountList accounts={accounts} widgetState={widgetState} removeAccount={removeAccount} editAccount={editAccount} />
            </section>
        </>
    );
};

export default AccountsWidget;
