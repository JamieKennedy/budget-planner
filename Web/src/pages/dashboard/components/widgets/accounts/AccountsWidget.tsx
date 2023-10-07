import { PlusIcon } from '@heroicons/react/20/solid';
import { useState } from 'react';
import useAccount from '../../../../../api/hooks/useAccount';
import Modal from '../../../../../components/misc/ui/pageElements/Modal';
import AccountList from './components/AccountList';
import AddEditAccount from './components/AddEditAccount';

const AccountsWidget = () => {
    const { accounts } = useAccount();

    const [addingAccount, setAddingAccount] = useState(false);

    return (
        <>
            {addingAccount && (
                <Modal closeFn={() => setAddingAccount(false)}>
                    <AddEditAccount closeFn={() => setAddingAccount(false)} />
                </Modal>
            )}
            <section className='h-fit w-full p-5'>
                <div className='relative'>
                    <div className='absolute inset-0 flex items-center' aria-hidden='true'>
                        <div className='w-full border-t border-gray-300' />
                    </div>
                    <div className='relative flex items-center justify-between'>
                        <span className='bg-white dark:bg-dark pr-3 text-xl font-semibold text-gray-900 dark:text-white'>Accounts</span>
                        <button
                            type='button'
                            className='inline-flex items-center gap-x-1.5 rounded-full bg-white dark:bg-dark px-3 py-1.5 text-sm font-semibold text-gray-900 dark:text-white shadow-md ring-1 ring-inset ring-gray-300 hover:bg-gray-50 dark:hover:bg-dark'
                            onClick={() => setAddingAccount(true)}
                        >
                            <PlusIcon className='-ml-1 -mr-0.5 h-5 w-5 text-gray-400' aria-hidden='true' />
                            <span>Add new account</span>
                        </button>
                    </div>
                </div>
                <AccountList accounts={accounts.data} widgetState={accounts.status} />
            </section>
        </>
    );
};

export default AccountsWidget;
