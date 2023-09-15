import Skeleton from "react-loading-skeleton";
import { TAccount } from "../../../../../../types/Accounts";
import { EWidgetState } from "../../../../../../types/Enum";
import AccountListItem from "./AccountListItem";

interface IAccountListProps {
    accounts: TAccount[];
    widgetState: EWidgetState;
    removeAccount: (account: TAccount) => void;
    editAccount: (account: TAccount) => void;
}

const AccountList = ({ accounts, widgetState, removeAccount, editAccount }: IAccountListProps) => {
    return (
        <div className='w-full h-fit flex flex-col items-center'>
            {widgetState === "Loading" && (
                <div className='flex flex-col m-5 w-full'>
                    <Skeleton className='my-2 h-10 rounded-md' count={3} />
                </div>
            )}

            {widgetState === "Loaded" && (
                <div className='flex flex-col w-full px-5 divide-y'>
                    {accounts
                        .sort((a, b) => a.balance - b.balance)
                        .map((account) => (
                            <AccountListItem key={account.id} account={account} removeAccount={removeAccount} editAccount={editAccount} />
                        ))}
                    <div className='flex flex-row py-2 justify-between pr-24'>
                        <p className='text-right italic text-gray-600 pr-5'>Total:</p>
                        <p className='italic text-gray-600 '>
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
