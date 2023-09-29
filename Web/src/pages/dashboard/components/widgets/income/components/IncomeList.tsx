import Skeleton from 'react-loading-skeleton';
import { EWidgetState } from '../../../../../../types/Enum';
import { TIncome } from '../../../../../../types/Income';
import IncomeListItem from './IncomeListItem';

interface IIncomeListProps {
    widgetState: EWidgetState;
    income: TIncome[];
}

const IncomeList = ({ widgetState, income }: IIncomeListProps) => {
    return (
        <div className='w-full h-fit flex flex-col items-center'>
            {widgetState === 'Loading' && (
                <div className='flex flex-col m-5 w-full'>
                    <Skeleton className='my-2 h-10 rounded-md' count={3} />
                </div>
            )}

            {widgetState === 'Loaded' && (
                <div className='flex flex-col w-full px-5 divide-y dark:text-white'>
                    {income
                        .sort((a, b) => a.amount - b.amount)
                        .map((income) => (
                            <IncomeListItem key={income.id} income={income} />
                        ))}
                    <div className='flex flex-row py-2 justify-between pr-24'>
                        <p className='text-right italic text-gray-600  dark:text-gray-300 pr-5'>Total:</p>
                        <p className='italic text-gray-600 dark:text-gray-300'>
                            £
                            {income
                                .map((income) => income.amount)
                                .reduce((acc: number, val: number) => acc + val, 0)
                                .toFixed(2)}
                        </p>
                    </div>
                </div>
            )}
        </div>
    );
};

export default IncomeList;
