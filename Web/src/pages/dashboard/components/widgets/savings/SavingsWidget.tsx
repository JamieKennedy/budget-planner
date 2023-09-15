import 'react-loading-skeleton/dist/skeleton.css';

import { useCallback, useEffect, useRef, useState } from 'react';
import { SavingsSchema, TSavings } from '../../../../../types/Savings';

import { PlusIcon } from '@heroicons/react/24/outline';
import Skeleton from 'react-loading-skeleton';
import { Savings } from '../../../../../api/Savings';
import Modal from '../../../../../components/misc/ui/pageElements/Modal';
import useApi from '../../../../../hooks/useApi';
import useAppStore from '../../../../../state/Store';
import { EPageState } from '../../../../../types/Enum';
import SavingsAddGoalForm from './components/SavingsAddGoalForm';
import SavingsItem from './components/SavingsItem';

const SavingsWidget = () => {
    const [pageState, setPageState] = useState<EPageState>('Loading');
    const [savingsData, setSavingsData] = useState<TSavings[] | null>(null);
    const [addingGoal, setAddingGoal] = useState(false);
    const user = useAppStore((appState) => appState.User);
    const [getSavings] = useApi<TSavings[], string>(
        Savings.GetSavingsByUserId,
        true
    );

    const fetch = useCallback(async () => {
        if (user) {
            const [savings, error] = await getSavings(
                user.id,
                SavingsSchema.array()
            );

            if (!error) {
                setSavingsData(savings);
                console.log(savings);
                setPageState('Loaded');
                return;
            }

            console.log(error);
        }

        setPageState('AuthError');
        return;
    }, [getSavings, user]);

    const isMounted = useRef(false);

    useEffect(() => {
        if (isMounted.current) return;

        if (pageState !== 'Loaded') {
            fetch();
            console.log('Feth savings');
        }

        isMounted.current = true;
    }, [fetch, pageState]);

    return (
        <>
            {user && addingGoal && (
                <Modal closeFn={() => setAddingGoal(false)}>
                    <SavingsAddGoalForm
                        savingsData={savingsData}
                        setSavingsData={setSavingsData}
                        closeFn={() => setAddingGoal(false)}
                        userId={user.id}
                    />
                </Modal>
            )}
            <section className="h-fit p-5">
                <div className="relative">
                    <div
                        className="absolute inset-0 flex items-center"
                        aria-hidden="true"
                    >
                        <div className="w-full border-t border-gray-300" />
                    </div>
                    <div className="relative flex items-center justify-between">
                        <span className="dark:bg-dark bg-white pr-3 text-xl font-semibold text-gray-900 dark:text-white">
                            Savings
                        </span>
                        <button
                            type="button"
                            className="inline-flex items-center gap-x-1.5 rounded-full bg-white dark:bg-dark px-3 py-1.5 text-sm font-semibold text-gray-900 dark:text-white shadow-md ring-1 ring-inset ring-gray-300 hover:bg-gray-50 dark:hover:bg-dark"
                            onClick={() => setAddingGoal(true)}
                        >
                            <PlusIcon
                                className="-ml-1 -mr-0.5 h-5 w-5 text-gray-400"
                                aria-hidden="true"
                            />
                            <span>Add new savings goal</span>
                        </button>
                    </div>
                </div>
                {pageState === 'Loaded' && savingsData && (
                    <div className="flex flex-col m-5">
                        {savingsData.map((item) => {
                            return (
                                <SavingsItem
                                    key={item.savingsId}
                                    item={item}
                                    savingsData={savingsData}
                                    setSavingsData={setSavingsData}
                                />
                            );
                        })}
                    </div>
                )}
                {pageState === 'Loading' && (
                    <div className="flex flex-col m-5">
                        <Skeleton
                            className="my-2 h-10 rounded-md"
                            count={savingsData ? savingsData.length : 2}
                        />
                    </div>
                )}
            </section>
        </>
    );
};

export default SavingsWidget;
