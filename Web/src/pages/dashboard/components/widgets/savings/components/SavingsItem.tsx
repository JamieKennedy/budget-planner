import { BiDotsHorizontalRounded, BiPencil, BiPlus, BiTrash } from 'react-icons/bi';

import moment from 'moment';
import { useState } from 'react';
import useSavings from '../../../../../../api/hooks/useSavings';
import ProgressBar from '../../../../../../components/misc/ui/ProgressBar';
import PopoverMenu from '../../../../../../components/misc/ui/interactable/PopoverMenu';
import Modal from '../../../../../../components/misc/ui/pageElements/Modal';
import { TSavings } from '../../../../../../types/Savings';
import { cn } from '../../../../../../utils/CssUtils';
import SavingsBalanceItem from './SavingsBalanceItem';
import SavingsBalanceAddForm from './SavingsBalnceAddForm';
import SavingsChart from './SavingsChart';
import SavingsEditGoalForm from './SavingsEditGoalForm';

interface ISavingsItemProps {
    item: TSavings;
}

const SavingsItem = ({ item }: ISavingsItemProps) => {
    const { deleteSavings } = useSavings();

    const [expanded, setExpanded] = useState(false);
    const [addingBalance, setAddingBalance] = useState(false);
    const [editingSavings, setEditingSavings] = useState(false);

    const currentBalance: number = (item?.savingsBalances?.length ?? 0) === 0 ? 0 : item.savingsBalances.sort((a, b) => b.created.getTime() - a.created.getTime())[0]?.balance ?? 0;

    const handleDelete = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.stopPropagation();
        deleteSavings.mutate(item.savingsId);
    };

    const handleAddingBalance = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.stopPropagation();

        setAddingBalance(true);
    };

    const handleEdit = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        e.stopPropagation();

        setEditingSavings(true);
    };

    const canExpand = item.description || (item.savingsBalances && item.savingsBalances.length > 0);

    return (
        <>
            {addingBalance && (
                <Modal closeFn={() => setAddingBalance(false)}>
                    <SavingsBalanceAddForm closeFn={() => setAddingBalance(false)} savingsId={item.savingsId} />
                </Modal>
            )}
            {editingSavings && (
                <Modal closeFn={() => setEditingSavings(false)}>
                    <SavingsEditGoalForm item={item} closeFn={() => setEditingSavings(false)} />
                </Modal>
            )}
            <div className='w-full min-h-fit rounded-md border-2 border-gray-300 my-2 shadow-md  items-center px-5 dark:text-white'>
                <div className='grid grid-cols-3 items-center' onClick={() => (canExpand ? setExpanded(!expanded) : null)}>
                    <div className='flex flex-row items-center h-10'>
                        <svg
                            xmlns='http://www.w3.org/2000/svg'
                            fill='none'
                            viewBox='0 0 24 24'
                            strokeWidth={2}
                            stroke='currentColor'
                            className={cn('w-4 h-4 mr-1 transition-transform duration-150 ', {
                                'rotate-90': expanded,
                            })}
                        >
                            <path strokeLinecap='round' strokeLinejoin='round' d='M8.25 4.5l7.5 7.5-7.5 7.5' />
                        </svg>
                        <p className='text-sm font-semibold'>{item.name}</p>
                    </div>

                    <div className='grid grid-cols-2 items-center w-96'>
                        <p className={`${item.goalDate ? 'col-span-1' : 'col-span-1'}`}>
                            £{currentBalance} / £{item.goal}
                        </p>

                        <ProgressBar percent={(currentBalance / item.goal) * 100} />
                    </div>

                    <div className='flex flex-row items-center gap-5 justify-end'>
                        {item.goalDate && <p className='justify-self-start'>{moment(item.goalDate).format('Do MMM yyyy')}</p>}
                        <PopoverMenu button={<BiDotsHorizontalRounded className='text-white h-6 w-6' />}>
                            <div className='flex flex-col divide-y divide-gray-600 w-full '>
                                <button onClick={(e) => handleAddingBalance(e)} type='button' className='flex flex-row gap-2 p-2 pr-4 hover:text-gray-200 items-center hover:bg-slate-600'>
                                    <BiPlus className='w-5 h-5 hover:text-gray-500 dark:hover:text-gray-200' />
                                    <p className='text-base'>Add balance</p>
                                </button>
                                <button onClick={(e) => handleEdit(e)} type='button' className='flex flex-row gap-2 p-2 pr-4 hover:text-gray-200 items-center hover:bg-slate-600'>
                                    <BiPencil className='w-5 h-5 hover:text-gray-500 dark:hover:text-gray-200' />
                                    <p className='text-base'>Edit</p>
                                </button>

                                <button onClick={(e) => handleDelete(e)} type='button' className='flex flex-row gap-2 p-2 pr-4 hover:text-gray-200 items-center hover:bg-slate-600'>
                                    <BiTrash className='w-5 h-5 hover:text-gray-500 dark:hover:text-gray-200' />
                                    <p className='text-base'>Delete</p>
                                </button>
                            </div>
                        </PopoverMenu>
                    </div>
                </div>

                {expanded && canExpand && (
                    <div className='pb-3 w-full'>
                        {item.description && <p className='justify-self-start'>{item.description}</p>}
                        {item.savingsBalances && item.savingsBalances.length > 0 && (
                            <>
                                <SavingsChart item={item} />
                                <h2 className='font-semibold'>Balance History</h2>
                                <div className='max-h-60 overflow-y-scroll mb-5 font-semibold scrollbar-thin scrollbar-thumb-gray-400 scrollbar-track-gray-300'>
                                    {item.savingsBalances.map((savingsBalance, index) => {
                                        return <SavingsBalanceItem key={savingsBalance.savingsBalanceId} item={savingsBalance} prevBalance={index === item.savingsBalances.length - 1 ? 0 : item.savingsBalances[index + 1].balance} />;
                                    })}
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
