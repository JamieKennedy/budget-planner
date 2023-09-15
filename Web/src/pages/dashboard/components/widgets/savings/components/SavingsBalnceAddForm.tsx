import { useCallback, useState } from "react";
import { SavingsBalanceCreateSchema, SavingsBalanceSchema, TSavingsBalance, TSavingsBalanceCreate } from "../../../../../../types/SavingsBalance";

import { useForm } from "react-hook-form";
import { BiPound } from "react-icons/bi";
import { SavingsBalance } from "../../../../../../api/SavingsBalance";
import FormErrorMessage from "../../../../../../components/misc/ui/FormErrorMessage";
import FormSubmitButton from "../../../../../../components/misc/ui/FormSubmitButton";
import useApi from "../../../../../../hooks/useApi";
import { EFormState } from "../../../../../../types/Enum";
import { TSavings } from "../../../../../../types/Savings";

interface ISavingsBalanceAddFormProps {
    savingsData: TSavings[];
    setSavingsData: React.Dispatch<React.SetStateAction<TSavings[] | null>>;
    savingsId: string;
    closeFn: () => void;
}

const SavingsBalanceAddForm = ({ savingsData, setSavingsData, savingsId, closeFn }: ISavingsBalanceAddFormProps) => {
    const [createSavingsBalance] = useApi<
        TSavingsBalance,
        {
            savingsId: string;
            data: TSavingsBalanceCreate;
        }
    >(SavingsBalance.CreateSavingsBalnce, true);
    const [formState, setFormState] = useState<EFormState>("Default");

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<TSavingsBalanceCreate>();

    const onSubmit = useCallback(
        async (data: TSavingsBalanceCreate) => {
            setFormState("Pending");

            if (!data.created) {
                data.created = undefined;
            }

            const result = SavingsBalanceCreateSchema.safeParse(data);
            console.log(result);

            if (result.success) {
                const [balance, error] = await createSavingsBalance({ savingsId: savingsId, data: result.data }, SavingsBalanceSchema);
                console.log(balance);

                if (!error) {
                    if (savingsData) {
                        setSavingsData(
                            savingsData.map((item) => {
                                if (item.savingsId !== savingsId) {
                                    return item;
                                }

                                return { ...item, savingsBalances: [...item.savingsBalances, balance] };
                            })
                        );
                    }
                }

                setFormState("Default");
                closeFn();
            }
        },
        [closeFn, createSavingsBalance, savingsData, savingsId, setSavingsData]
    );

    return (
        <div
            className={"border-2 border-gray-200 bg-white rounded-2xl shadow-xl overflow-hidden max-w-xl w-full h-fit flex flex-col p-5 pb-10"}
            onClick={(e) => {
                e.stopPropagation();
            }}
        >
            <div className='flex flex-row items-center justify-between'>
                <h2 className='text-xl font-semibold'>Add Balance</h2>
                <button onClick={() => closeFn()}>
                    <svg
                        xmlns='http://www.w3.org/2000/svg'
                        fill='none'
                        viewBox='0 0 24 24'
                        strokeWidth={1.5}
                        stroke='currentColor'
                        className='w-6 h-6 hover:text-gray-500'
                    >
                        <path strokeLinecap='round' strokeLinejoin='round' d='M6 18L18 6M6 6l12 12' />
                    </svg>
                </button>
            </div>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className='h-fit'>
                    <div className='h-24'>
                        <label htmlFor='balance' className='block text-sm font-medium leading-6 e'>
                            Amount
                        </label>
                        <div className='relative  rounded-md shadow-sm'>
                            <div className='pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3'>
                                <BiPound className='h-5 w-5 text-gray-400' aria-hidden='true' />
                            </div>
                            <input
                                {...register("balance", {
                                    required: "balance is required",
                                    pattern: { value: new RegExp("^[0-9]\\d*([\\,\\.]\\d{2})?$"), message: "Invalid Balance" },
                                })}
                                id='balance'
                                type='text'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-10 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.balance ? "true" : "false"}
                            />
                        </div>
                        {errors.balance && errors.balance.message && <FormErrorMessage message={errors.balance.message} />}
                    </div>
                    <div className='h-24'>
                        <label htmlFor='date' className='block text-sm font-medium leading-6 '>
                            Date
                        </label>
                        <div className='relativerounded-md shadow-sm'>
                            <input
                                {...register("created")}
                                id='date'
                                type='date'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-10 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.created ? "true" : "false"}
                            />
                        </div>
                        {errors.created && errors.created.message && <FormErrorMessage message={errors.created.message} />}
                    </div>

                    <div className='mt-3'>
                        <FormSubmitButton
                            defaultStateText='Save'
                            formState={formState}
                            className='flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600 mb-2'
                        />
                    </div>
                </div>
            </form>
        </div>
    );
};

export default SavingsBalanceAddForm;
