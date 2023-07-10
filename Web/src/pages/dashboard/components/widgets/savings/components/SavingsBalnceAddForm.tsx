import { useCallback } from "react";
import { useForm } from "react-hook-form";
import FormErrorMessage from "../../../../../../components/misc/ui/FormErrorMessage";
import FormSubmitButton from "../../../../../../components/misc/ui/FormSubmitButton";
import { FormState } from "../../../../../../types/Enum";
import { TSavingsBalanceCreate } from "../../../../../../types/SavingsBalance";

interface ISavingsBalanceAddFormProps {
    addBalance: (newBalance: TSavingsBalanceCreate) => void;
}

const SavingsBalanceAddForm = ({ addBalance }: ISavingsBalanceAddFormProps) => {
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<TSavingsBalanceCreate>();

    const onSubmit = useCallback(
        async (data: TSavingsBalanceCreate) => {
            addBalance(data);
        },
        [addBalance]
    );

    return (
        <div
            className={"border-2 border-gray-200 bg-white rounded-2xl shadow-xl overflow-hidden max-w-xl w-full h-72 flex flex-col p-5"}
            onClick={(e) => {
                e.stopPropagation();
            }}
        >
            <h2 className='text-xl font-semibold'>Latest Balance</h2>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className='h-24'>
                    <label htmlFor='balance' className='block text-sm font-medium leading-6 text-white'>
                        Balance
                    </label>
                    <div className='mt-2'>
                        <input
                            {...register("balance", { required: "balance is required" })}
                            id='balance'
                            type='text'
                            className='block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6 mb-2'
                            aria-invalid={errors.balance ? "true" : "false"}
                        />
                        {errors.balance && errors.balance.message && <FormErrorMessage message={errors.balance.message} />}
                    </div>

                    <div className='h-24'>
                        <FormSubmitButton
                            defaultStateText='Save'
                            formState={FormState.Default}
                            className='flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600 mb-2'
                        />
                    </div>
                </div>
            </form>
        </div>
    );
};

export default SavingsBalanceAddForm;
