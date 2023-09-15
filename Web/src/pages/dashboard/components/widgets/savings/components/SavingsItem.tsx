import { useCallback, useState } from 'react';
import { BiPencil, BiPlus, BiTrash } from 'react-icons/bi';

import moment from 'moment';
import { Savings } from '../../../../../../api/Savings';
import ProgressBar from '../../../../../../components/misc/ui/ProgressBar';
import Modal from '../../../../../../components/misc/ui/pageElements/Modal';
import useApi from '../../../../../../hooks/useApi';
import useAppStore from '../../../../../../state/Store';
import { TSavings } from '../../../../../../types/Savings';
import SavingsBalanceItem from './SavingsBalanceItem';
import SavingsBalanceAddForm from './SavingsBalnceAddForm';
import SavingsChart from './SavingsChart';
import SavingsEditGoalForm from './SavingsEditGoalForm';

interface ISavingsItemProps {
    savingsData: TSavings[];
    setSavingsData: React.Dispatch<React.SetStateAction<TSavings[] | null>>;
    item: TSavings;
}

const SavingsItem = ({
    item,
    savingsData,
    setSavingsData,
}: ISavingsItemProps) => {
    const [expanded, setExpanded] = useState(false);
    const [addingBalance, setAddingBalance] = useState(false);
    const [editingSavings, setEditingSavings] = useState(false);
    const [user, setError] = useAppStore((appState) => [
        appState.User,
        appState.setError,
    ]);

    const [deleteSavings] = useApi<void, { userId: string; savingsId: string }>(
        Savings.Delete,
        true
    );

    const currentBalance: number =
        (item?.savingsBalances?.length ?? 0) === 0
            ? 0
            : item.savingsBalances.sort(
                  (a, b) => b.created.getTime() - a.created.getTime()
              )[0]?.balance ?? 0;

    const handleAdd = useCallback(
        async (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
            e.stopPropagation();

            setAddingBalance(true);
        },
        []
    );

    const handleEdit = useCallback(
        async (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
            e.stopPropagation();
            setEditingSavings(true);
        },
        []
    );

    const handleDelete = useCallback(
        async (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
            e.stopPropagation();

            if (user) {
                const [, error] = await deleteSavings({
                    userId: user?.id,
                    savingsId: item.savingsId,
                });

                if (!error) {
                    setSavingsData(
                        savingsData.filter(
                            (stateItem) =>
                                stateItem.savingsId !== item.savingsId
                        )
                    );
                    return;
                }

                setError(error.Message);
            }
        },
        [
            deleteSavings,
            item.savingsId,
            savingsData,
            setError,
            setSavingsData,
            user,
        ]
    );

    const canExpand =
        item.description ||
        (item.savingsBalances && item.savingsBalances.length > 0);

    return (
        <>
            {addingBalance && (
                <Modal closeFn={() => setAddingBalance(false)}>
                    <SavingsBalanceAddForm
                        closeFn={() => setAddingBalance(false)}
                        savingsId={item.savingsId}
                        savingsData={savingsData}
                        setSavingsData={setSavingsData}
                    />
                </Modal>
            )}
            {editingSavings && (
                <Modal closeFn={() => setEditingSavings(false)}>
                    <SavingsEditGoalForm
                        item={item}
                        closeFn={() => setEditingSavings(false)}
                        savingsData={savingsData}
                        setSavingsData={setSavingsData}
                    />
                </Modal>
            )}
            <div className="w-full min-h-fit rounded-md border-2 border-gray-300 my-2 shadow-md  items-center px-5 dark:text-white">
                <div
                    className="grid grid-cols-3 items-center"
                    onClick={() => (canExpand ? setExpanded(!expanded) : null)}
                >
                    <div className="flex flex-row items-center h-10">
                        <svg
                            xmlns="http://www.w3.org/2000/svg"
                            fill="none"
                            viewBox="0 0 24 24"
                            strokeWidth={2}
                            stroke="currentColor"
                            className={`w-4 h-4 mr-1 hover:text-gray-500 transition-transform duration-75 ease-in-out ${
                                expanded ? 'rotate-90' : ''
                            }`}
                        >
                            <path
                                strokeLinecap="round"
                                strokeLinejoin="round"
                                d="M8.25 4.5l7.5 7.5-7.5 7.5"
                            />
                        </svg>
                        <p className="text-sm font-semibold">{item.name}</p>
                    </div>

                    <div className="grid grid-cols-2 items-center w-96">
                        <p
                            className={`${
                                item.goalDate ? 'col-span-1' : 'col-span-1'
                            }`}
                        >
                            £{currentBalance} / £{item.goal}
                        </p>

                        <ProgressBar
                            percent={(currentBalance / item.goal) * 100}
                        />
                    </div>

                    <div className="flex flex-row items-center gap-5 justify-end">
                        {item.goalDate && (
                            <p className="justify-self-start">
                                {moment(item.goalDate).format('Do MMM yyyy')}
                            </p>
                        )}
                        <div className="flex flex-row gap-3">
                            <button onClick={(e) => handleAdd(e)}>
                                <BiPlus className="w-6 h-6 hover:text-gray-500 dark:hover:text-gray-200" />
                            </button>
                            <button onClick={(e) => handleEdit(e)}>
                                <BiPencil className="w-6 h-6 hover:text-gray-500 dark:hover:text-gray-200" />
                            </button>

                            <button onClick={(e) => handleDelete(e)}>
                                <BiTrash className="w-6 h-6 hover:text-gray-500 dark:hover:text-gray-200" />
                            </button>
                        </div>
                    </div>
                </div>

                {expanded && canExpand && (
                    <div className="pb-3 w-full">
                        {item.description && (
                            <p className="justify-self-start">
                                {item.description}
                            </p>
                        )}
                        {item.savingsBalances &&
                            item.savingsBalances.length > 0 && (
                                <>
                                    <SavingsChart item={item} />
                                    <h2 className="font-semibold">
                                        Balance History
                                    </h2>
                                    <div className="max-h-60 overflow-y-scroll mb-5 font-semibold scrollbar-thin scrollbar-thumb-gray-400 scrollbar-track-gray-300">
                                        {item.savingsBalances.map(
                                            (savingsBalance, index) => {
                                                return (
                                                    <SavingsBalanceItem
                                                        key={
                                                            savingsBalance.savingsBalanceId
                                                        }
                                                        item={savingsBalance}
                                                        savingsData={
                                                            savingsData
                                                        }
                                                        setSavingsData={
                                                            setSavingsData
                                                        }
                                                        prevBalance={
                                                            index ===
                                                            item.savingsBalances
                                                                .length -
                                                                1
                                                                ? 0
                                                                : item
                                                                      .savingsBalances[
                                                                      index + 1
                                                                  ].balance
                                                        }
                                                    />
                                                );
                                            }
                                        )}
                                    </div>
                                </>
                            )}
                    </div>
                )}
            </div>
        </>
    );
};

export default SavingsItem;
