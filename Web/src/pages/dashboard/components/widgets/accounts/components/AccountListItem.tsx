import { BiDotsHorizontalRounded, BiPencil, BiTrash } from 'react-icons/bi';

import { useState } from 'react';
import useAccount from '../../../../../../api/hooks/useAccount';
import PopoverMenu from '../../../../../../components/misc/ui/interactable/PopoverMenu';
import Modal from '../../../../../../components/misc/ui/pageElements/Modal';
import useAppStore from '../../../../../../state/Store';
import { TAccount } from '../../../../../../types/Accounts';
import AddEditAccount from './AddEditAccount';

interface IAccountListItemProps {
    account: TAccount;
}

const AccountListItem = ({ account }: IAccountListItemProps) => {
    const [setError] = useAppStore((state) => [state.setError]);

    const { deleteAccount } = useAccount();

    const [edittingAccount, setEditingAccount] = useState(false);

    const handleDelete = async () => {
        deleteAccount.mutate(account.id, {
            onError(error) {
                setError(error.Message);
            },
        });
    };

    return (
        <>
            {edittingAccount && (
                <Modal closeFn={() => setEditingAccount(false)}>
                    <AddEditAccount account={account} closeFn={() => setEditingAccount(false)} />
                </Modal>
            )}
            <div className='flex flex-row w-full py-2 '>
                <p className=' text-base font-semibold'>{account.name}</p>
                <p className='ml-auto'>£{account.balance}</p>
                <div className='flex flex-row w-24 items-center justify-end gap-2'>
                    <PopoverMenu button={<BiDotsHorizontalRounded className='text-white h-6 w-6' />}>
                        <div className='flex flex-col divide-y divide-gray-600 w-full'>
                            <button onClick={() => setEditingAccount(true)} className='flex flex-row gap-2 p-2 pr-4 hover:text-gray-200 items-center hover:bg-slate-600'>
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
