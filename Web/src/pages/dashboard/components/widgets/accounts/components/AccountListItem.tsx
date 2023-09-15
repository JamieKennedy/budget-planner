import { useState } from 'react';
import { Accounts } from '../../../../../../api/Accounts';
import Modal from '../../../../../../components/misc/ui/pageElements/Modal';
import useApi from '../../../../../../hooks/useApi';
import useAppStore from '../../../../../../state/Store';
import { TAccount } from '../../../../../../types/Accounts';
import AddEditAccount from './AddEditAccount';

interface IAccountListItemProps {
    account: TAccount;
    removeAccount: (account: TAccount) => void;
    editAccount: (account: TAccount) => void;
}

const AccountListItem = ({
    account,
    removeAccount,
    editAccount,
}: IAccountListItemProps) => {
    const [setError] = useAppStore((state) => [state.setError]);

    const [deleteAccount] = useApi<void, { userId: string; accountId: string }>(
        Accounts.DeleteAccount
    );

    const [edittingAccount, setEdditingAccount] = useState(false);

    const handleDelete = async () => {
        const [, error] = await deleteAccount({
            userId: account.userId,
            accountId: account.id,
        });

        if (error) {
            setError(error.Message);
            return;
        }

        removeAccount(account);
    };

    console.log(account);
    return (
        <>
            {edittingAccount && (
                <Modal closeFn={() => setEdditingAccount(false)}>
                    <AddEditAccount
                        account={account}
                        editAccount={editAccount}
                        closeFn={() => setEdditingAccount(false)}
                    />
                </Modal>
            )}
            <div className="flex flex-row w-full py-2 ">
                <p className=" text-base font-semibold">{account.name}</p>
                <p className="ml-auto">£{account.balance}</p>
                <div className="flex flex-row w-24 items-center justify-end gap-2">
                    <button onClick={() => setEdditingAccount(true)}>
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            strokeWidth={1.5}
                            stroke="currentColor"
                            className="w-6 h-6 hover:text-gray-500 dark:hover:text-gray-200"
                        >
                            <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                d="M10.5 6h9.75M10.5 6a1.5 1.5 0 11-3 0m3 0a1.5 1.5 0 10-3 0M3.75 6H7.5m3 12h9.75m-9.75 0a1.5 1.5 0 01-3 0m3 0a1.5 1.5 0 00-3 0m-3.75 0H7.5m9-6h3.75m-3.75 0a1.5 1.5 0 01-3 0m3 0a1.5 1.5 0 00-3 0m-9.75 0h9.75"
                            />
                        </svg>
                    </button>

                    <button onClick={() => handleDelete()}>
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            strokeWidth={1.5}
                            stroke="currentColor"
                            className="w-6 h-6 hover:text-gray-500 dark:hover:text-gray-200"
                        >
                            <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                d="M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0"
                            />
                        </svg>
                    </button>
                </div>
            </div>
        </>
    );
};

export default AccountListItem;
