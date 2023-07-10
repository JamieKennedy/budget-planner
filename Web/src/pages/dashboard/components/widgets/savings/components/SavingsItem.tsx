import { useCallback, useState } from "react";

import ProgressBar from "../../../../../../components/misc/ui/ProgressBar";
import Modal from "../../../../../../components/misc/ui/pageElements/Modal";
import { TSavings } from "../../../../../../types/Savings";
import { TSavingsBalanceCreate } from "../../../../../../types/SavingsBalance";
import SavingsBalanceAddForm from "./SavingsBalnceAddForm";

interface ISavingsItemProps {
    addBalance: (savingsId: string, newBalance: TSavingsBalanceCreate) => Promise<void>;
    item: TSavings;
}

const SavingsItem = ({ item, addBalance }: ISavingsItemProps) => {
    const [expanded, setExpanded] = useState(false);
    const [addingBalance, setAddingBalance] = useState(false);

    const currentBalance: number =
        (item?.savingsBalances?.length ?? 0) === 0 ? 0 : item.savingsBalances.sort((a, b) => b.created.getTime() - a.created.getTime())[0]?.balance ?? 0;

    const handleAdd = useCallback(async (e: React.MouseEvent<SVGSVGElement, MouseEvent>) => {
        e.stopPropagation();

        setAddingBalance(true);
    }, []);

    const addNewBalance = useCallback(
        async (newBalance: TSavingsBalanceCreate) => {
            addBalance(item.savingsId, newBalance).then(() => {
                setAddingBalance(false);
            });
        },
        [addBalance, item.savingsId]
    );

    const handleEdit = useCallback(async (e: React.MouseEvent<SVGSVGElement, MouseEvent>) => {
        e.stopPropagation();
    }, []);

    const handleDelete = useCallback(async (e: React.MouseEvent<SVGSVGElement, MouseEvent>) => {
        e.stopPropagation();
    }, []);

    return (
        <>
            {addingBalance && (
                <Modal closeFn={() => setAddingBalance(false)}>
                    <SavingsBalanceAddForm addBalance={addNewBalance} closeFn={() => setAddingBalance(false)} />
                </Modal>
            )}
            <div className='w-full min-h-fit rounded-md border-2 border-gray-300 my-2 shadow-md  items-center px-5 '>
                <div className='grid grid-cols-3 items-center' onClick={() => setExpanded(!expanded)}>
                    <div className='flex flex-row items-center h-10'>
                        <svg
                            xmlns='http://www.w3.org/2000/svg'
                            fill='none'
                            viewBox='0 0 24 24'
                            strokeWidth={2}
                            stroke='currentColor'
                            className={`w-4 h-4 mr-1 hover:text-gray-500 transition-transform duration-75 ease-in-out ${expanded ? "rotate-90" : ""}`}
                        >
                            <path strokeLinecap='round' strokeLinejoin='round' d='M8.25 4.5l7.5 7.5-7.5 7.5' />
                        </svg>
                        <p className='text-sm font-semibold'>{item.name}</p>
                    </div>

                    <div className='grid grid-cols-2 items-center w-72'>
                        <p>
                            £{currentBalance} / £{item.goal}
                        </p>
                        <ProgressBar percent={(currentBalance / item.goal) * 100} />
                    </div>
                    <div className='flex flex-row items-center justify-self-end gap-2'>
                        <svg
                            xmlns='http://www.w3.org/2000/svg'
                            fill='none'
                            viewBox='0 0 24 24'
                            strokeWidth={1.5}
                            stroke='currentColor'
                            className='w-6 h-6 hover:text-gray-500 pointer-events-auto'
                            onClick={(e) => handleAdd(e)}
                        >
                            <path strokeLinecap='round' strokeLinejoin='round' d='M12 9v6m3-3H9m12 0a9 9 0 11-18 0 9 9 0 0118 0z' />
                        </svg>

                        <svg
                            xmlns='http://www.w3.org/2000/svg'
                            fill='none'
                            viewBox='0 0 24 24'
                            strokeWidth={1.5}
                            stroke='currentColor'
                            className='w-6 h-6 hover:text-gray-500'
                            onClick={(e) => handleEdit(e)}
                        >
                            <path
                                strokeLinecap='round'
                                strokeLinejoin='round'
                                d='M10.5 6h9.75M10.5 6a1.5 1.5 0 11-3 0m3 0a1.5 1.5 0 10-3 0M3.75 6H7.5m3 12h9.75m-9.75 0a1.5 1.5 0 01-3 0m3 0a1.5 1.5 0 00-3 0m-3.75 0H7.5m9-6h3.75m-3.75 0a1.5 1.5 0 01-3 0m3 0a1.5 1.5 0 00-3 0m-9.75 0h9.75'
                            />
                        </svg>

                        <svg
                            xmlns='http://www.w3.org/2000/svg'
                            fill='none'
                            viewBox='0 0 24 24'
                            strokeWidth={1.5}
                            stroke='currentColor'
                            className='w-6 h-6 hover:text-gray-500'
                            onClick={(e) => handleDelete(e)}
                        >
                            <path
                                strokeLinecap='round'
                                strokeLinejoin='round'
                                d='M14.74 9l-.346 9m-4.788 0L9.26 9m9.968-3.21c.342.052.682.107 1.022.166m-1.022-.165L18.16 19.673a2.25 2.25 0 01-2.244 2.077H8.084a2.25 2.25 0 01-2.244-2.077L4.772 5.79m14.456 0a48.108 48.108 0 00-3.478-.397m-12 .562c.34-.059.68-.114 1.022-.165m0 0a48.11 48.11 0 013.478-.397m7.5 0v-.916c0-1.18-.91-2.164-2.09-2.201a51.964 51.964 0 00-3.32 0c-1.18.037-2.09 1.022-2.09 2.201v.916m7.5 0a48.667 48.667 0 00-7.5 0'
                            />
                        </svg>
                    </div>
                </div>

                {expanded && <div className='w-full h-72 col-span-3'></div>}
            </div>
        </>
    );
};

export default SavingsItem;