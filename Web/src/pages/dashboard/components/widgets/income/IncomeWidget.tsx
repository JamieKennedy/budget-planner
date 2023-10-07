import { PlusIcon } from '@heroicons/react/20/solid';
import useIncome from '../../../../../api/hooks/useIncome';
import IncomeList from './components/IncomeList';

interface IIncomeWidgetProps {}

const IncomeWidget = ({}: IIncomeWidgetProps) => {
    const { income } = useIncome();

    return (
        <>
            <section className='h-fit w-full p-5'>
                <div className='relative'>
                    <div className='absolute inset-0 flex items-center' aria-hidden='true'>
                        <div className='w-full border-t border-gray-300' />
                    </div>
                    <div className='relative flex items-center justify-between'>
                        <span className='bg-white dark:bg-dark pr-3 text-xl font-semibold text-gray-900 dark:text-white'>Income</span>
                        <button
                            type='button'
                            className='inline-flex items-center gap-x-1.5 rounded-full bg-white dark:bg-dark px-3 py-1.5 text-sm font-semibold text-gray-900 dark:text-white shadow-md ring-1 ring-inset ring-gray-300 hover:bg-gray-50 dark:hover:bg-dark'
                            onClick={() => {}}
                        >
                            <PlusIcon className='-ml-1 -mr-0.5 h-5 w-5 text-gray-400' aria-hidden='true' />
                            <span>Add new account</span>
                        </button>
                    </div>
                </div>
                <IncomeList widgetState={income.status} income={income.data} />
            </section>
        </>
    );
};

export default IncomeWidget;
