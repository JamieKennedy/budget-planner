import 'react-loading-skeleton/dist/skeleton.css';

import { PlusIcon } from '@heroicons/react/24/outline';
import { useState } from 'react';
import Skeleton from 'react-loading-skeleton';
import useSavings from '../../../../../api/hooks/useSavings';
import Modal from '../../../../../components/misc/ui/pageElements/Modal';
import SavingsAddGoalForm from './components/SavingsAddGoalForm';
import SavingsItem from './components/SavingsItem';

const SavingsWidget = () => {
    const [addingGoal, setAddingGoal] = useState(false);

    const { savings } = useSavings();

    return (
        <>
            {addingGoal && (
                <Modal closeFn={() => setAddingGoal(false)}>
                    <SavingsAddGoalForm closeFn={() => setAddingGoal(false)} />
                </Modal>
            )}
            <section className='h-fit p-5'>
                <div className='relative'>
                    <div className='absolute inset-0 flex items-center' aria-hidden='true'>
                        <div className='w-full border-t border-gray-300' />
                    </div>
                    <div className='relative flex items-center justify-between'>
                        <span className='dark:bg-dark bg-white pr-3 text-xl font-semibold text-gray-900 dark:text-white'>Savings</span>
                        <button
                            type='button'
                            className='inline-flex items-center gap-x-1.5 rounded-full bg-white dark:bg-dark px-3 py-1.5 text-sm font-semibold text-gray-900 dark:text-white shadow-md ring-1 ring-inset ring-gray-300 hover:bg-gray-50 dark:hover:bg-dark'
                            onClick={() => setAddingGoal(true)}
                        >
                            <PlusIcon className='-ml-1 -mr-0.5 h-5 w-5 text-gray-400' aria-hidden='true' />
                            <span>Add new savings goal</span>
                        </button>
                    </div>
                </div>
                {savings.status === 'success' && (
                    <div className='flex flex-col m-5'>
                        {savings.data.map((item) => {
                            return <SavingsItem key={item.savingsId} item={item} />;
                        })}
                    </div>
                )}
                {savings.status !== 'success' && (
                    <div className='flex flex-col m-5'>
                        <Skeleton className='my-2 h-10 rounded-md' count={2} />
                    </div>
                )}
            </section>
        </>
    );
};

export default SavingsWidget;
