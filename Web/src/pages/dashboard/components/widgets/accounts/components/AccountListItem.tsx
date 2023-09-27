import { BiDotsHorizontalRounded, BiPencil, BiTrash } from 'react-icons/bi';

import { useState } from 'react';
import { Accounts } from '../../../../../../api/Accounts';
import PopoverMenu from '../../../../../../components/misc/ui/interactable/PopoverMenu';
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

const AccountListItem = ({ account, removeAccount, editAccount }: IAccountListItemProps) => {
    const [setError] = useAppStore((state) => [state.setError]);

    const [deleteAccount] = useApi<void, { userId: string; accountId: string }>(Accounts.DeleteAccount);

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
                    <AddEditAccount account={account} editAccount={editAccount} closeFn={() => setEdditingAccount(false)} />
                </Modal>
            )}
            <div className='flex flex-row w-full py-2 '>
                <p className=' text-base font-semibold'>{account.name}</p>
                <p className='ml-auto'>£{account.balance}</p>
                <div className='flex flex-row w-24 items-center justify-end gap-2'>
                    <PopoverMenu button={<BiDotsHorizontalRounded className='text-white h-6 w-6' />}>
                        <div className='flex flex-col divide-y divide-gray-600 w-full'>
                            <button onClick={() => setEdditingAccount(true)} className='flex flex-row gap-2 p-2 pr-4 hover:text-gray-200 items-center hover:bg-slate-600'>
                                <BiPencil className='w-5 h-5 hover:text-gray-500 dark:hover:text-gray-200' />
                                <p className='text-base'>Edit</p>
                            </button>

                            <button onClick={() => handleDelete()} className='flex flex-row gap-2 p-2 pr-4 hover:text-gray-200 items-center hover:bg-slate-600 '>
                                <BiTrash className='w-5 h-5 hover:text-gray-500 dark:hover:text-gray-200' />
                                <p>Delete</p>
                            </button>
                        </div>
                    </PopoverMenu>
                </div>
            </div>
        </>
    );
};

export default AccountListItem;
