import moment from "moment";
import { useCallback } from "react";
import { SavingsBalance } from "../../../../../../api/SavingsBalance";
import useApi from "../../../../../../hooks/useApi";
import useAppStore from "../../../../../../state/Store";
import { ETrendDirection } from "../../../../../../types/Enum";
import { TSavings } from "../../../../../../types/Savings";
import { TSavingsBalance } from "../../../../../../types/SavingsBalance";

interface ISavingsBalanceItemProps {
    prevBalance: number;
    item: TSavingsBalance;
    savingsData: TSavings[];
    setSavingsData: React.Dispatch<React.SetStateAction<TSavings[] | null>>;
}

const SavingsBalanceItem = ({ prevBalance, item, savingsData, setSavingsData }: ISavingsBalanceItemProps) => {
    const setError = useAppStore((appState) => appState.setError);
    const [deleteSasvingsBalance] = useApi<void, { savingsId: string; savingsBalanceId: string }>(SavingsBalance.DeleteSavingsBalance, true);

    const trendDirection: ETrendDirection = prevBalance < item.balance ? "Up" : prevBalance > item.balance ? "Down" : "Level";

    const handleDelete = useCallback(async () => {
        const [, error] = await deleteSasvingsBalance({ savingsId: item.savingsId, savingsBalanceId: item.savingsBalanceId });

        if (error) {
            setError(error.Message);
            return;
        }

        setSavingsData(
            savingsData.map((savingsDataItem) => {
                if (savingsDataItem.savingsId !== item.savingsId) {
                    return savingsDataItem;
                }

                return {
                    ...savingsDataItem,
                    savingsBalances: savingsDataItem.savingsBalances.filter(
                        (savingsBalanceItem) => savingsBalanceItem.savingsBalanceId !== item.savingsBalanceId
                    ),
                };
            })
        );
    }, [deleteSasvingsBalance, item.savingsBalanceId, item.savingsId, savingsData, setError, setSavingsData]);

    return (
        <div className='grid grid-cols-4 w-full my-2 bg-gray-100 px-5 py-2 rounded-md'>
            <p>£{item.balance}</p>
            <p>{moment(item.created).format("YY/MM/DD HH:mm")}</p>
            <div className='justify-self-start'>
                {trendDirection === "Up" ? (
                    <svg
                        xmlns='http://www.w3.org/2000/svg'
                        fill='none'
                        viewBox='0 0 24 24'
                        strokeWidth={1.5}
                        stroke='currentColor'
                        className='w-6 h-6 text-green-500'
                    >
                        <path
                            strokeLinecap='round'
                            strokeLinejoin='round'
                            d='M2.25 18L9 11.25l4.306 4.307a11.95 11.95 0 015.814-5.519l2.74-1.22m0 0l-5.94-2.28m5.94 2.28l-2.28 5.941'
                        />
                    </svg>
                ) : trendDirection === "Down" ? (
                    <svg
                        xmlns='http://www.w3.org/2000/svg'
                        fill='none'
                        viewBox='0 0 24 24'
                        strokeWidth={1.5}
                        stroke='currentColor'
                        className='w-6 h-6 text-red-800'
                    >
                        <path
                            strokeLinecap='round'
                            strokeLinejoin='round'
                            d='M2.25 6L9 12.75l4.286-4.286a11.948 11.948 0 014.306 6.43l.776 2.898m0 0l3.182-5.511m-3.182 5.51l-5.511-3.181'
                        />
                    </svg>
                ) : (
                    <svg
                        xmlns='http://www.w3.org/2000/svg'
                        fill='none'
                        viewBox='0 0 24 24'
                        strokeWidth={1.5}
                        stroke='currentColor'
                        className='w-6 h-6 text-gray-500'
                    >
                        <path strokeLinecap='round' strokeLinejoin='round' d='M3.75 9h16.5m-16.5 6.75h16.5' />
                    </svg>
                )}
            </div>
            <button className='justify-self-end'>
                <svg
                    xmlns='http://www.w3.org/2000/svg'
                    fill='none'
                    viewBox='0 0 24 24'
                    strokeWidth={1.5}
                    stroke='currentColor'
                    className='w-6 h-6 hover:text-gray-500'
                    onClick={() => handleDelete()}
                >
                    <path
                        strokeLinecap='round'
                        strokeLinejoin='round'
                        d='M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0'
                    />
                </svg>
            </button>
        </div>
    );
};

export default SavingsBalanceItem;
