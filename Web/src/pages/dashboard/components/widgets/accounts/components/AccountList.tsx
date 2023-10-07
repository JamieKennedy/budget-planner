import Skeleton from 'react-loading-skeleton';
import { TAccount } from '../../../../../../types/Accounts';
import { EQueryStatus } from '../../../../../../types/Enum';
import AccountListItem from './AccountListItem';

interface IAccountListProps {
    accounts: TAccount[];
    widgetState: EQueryStatus;
}

const AccountList = ({ accounts, widgetState }: IAccountListProps) => {
    return (
        <div className='w-full h-fit flex flex-col items-center'>
            {widgetState === 'pending' && (
                <div className='flex flex-col m-5 w-full'>
                    <Skeleton className='my-2 h-10 rounded-md' count={3} />
                </div>
            )}

            {widgetState === 'success' && (
                <div className='flex flex-col w-full px-5 divide-y dark:text-white'>
                    {accounts
                        .sort((a, b) => a.balance - b.balance)
                        .map((account) => (
                            <AccountListItem key={account.id} account={account} />
                        ))}
                    <div className='flex flex-row py-2 justify-between pr-24'>
                        <p className='text-right italic text-gray-600  dark:text-gray-300 pr-5'>Total:</p>
                        <p className='italic text-gray-600 dark:text-gray-300'>
                            £
                            {accounts
                                .map((account) => account.balance)
                                .reduce((acc: number, val: number) => acc + val, 0)
                                .toFixed(2)}
                        </p>
                    </div>
                </div>
            )}
        </div>
    );
};

export default AccountList;
