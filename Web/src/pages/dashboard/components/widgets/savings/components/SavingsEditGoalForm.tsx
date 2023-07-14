import { useCallback, useState } from "react";
import { SavingsEditSchema, SavingsSchema, TSavings, TSavingsEdit } from "../../../../../../types/Savings";

import moment from "moment";
import { useForm } from "react-hook-form";
import { BiPound } from "react-icons/bi";
import { Savings } from "../../../../../../api/Savings";
import FormErrorMessage from "../../../../../../components/misc/ui/FormErrorMessage";
import FormSubmitButton from "../../../../../../components/misc/ui/FormSubmitButton";
import useApi from "../../../../../../hooks/useApi";
import useAppStore from "../../../../../../state/Store";
import { FormState } from "../../../../../../types/Enum";

interface ISavingsEditGoalFormProps {
    item: TSavings;
    savingsData: TSavings[];
    setSavingsData: React.Dispatch<React.SetStateAction<TSavings[] | null>>;
    closeFn: () => void;
}

const SavingsEditGoalForm = ({ item, savingsData, setSavingsData, closeFn }: ISavingsEditGoalFormProps) => {
    const [formState, setFormState] = useState<FormState>(FormState.Default);
    const setError = useAppStore((appState) => appState.setError);
    const [editSavings] = useApi<TSavings, TSavingsEdit>(Savings.Edit, true);

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<TSavingsEdit>();

    const onSubmit = useCallback(
        async (data: TSavingsEdit) => {
            setFormState(FormState.Pending);

            if (!data.goalDate) {
                data.goalDate = null;
            }

            data.userId = item.userId;
            data.savingsId = item.savingsId;

            const parseResult = SavingsEditSchema.safeParse(data);

            if (parseResult.success) {
                const [updatedGoal, error] = await editSavings(parseResult.data, SavingsSchema);

                if (error) {
                    setError(error.Message);
                    setFormState(FormState.Default);
                    return;
                }

                setSavingsData(
                    savingsData.map((savingsDataItem) => {
                        if (savingsDataItem.savingsId !== item.savingsId) {
                            return savingsDataItem;
                        }

                        return updatedGoal;
                    })
                );

                setFormState(FormState.Default);
                closeFn();
            } else {
                console.log(parseResult.error);
                setFormState(FormState.Default);
            }
        },
        [closeFn, editSavings, item.savingsId, item.userId, savingsData, setError, setSavingsData]
    );

    return (
        <div
            className={"border-2 border-gray-200 bg-white rounded-2xl shadow-xl overflow-hidden max-w-xl w-full h-fit flex flex-col p-5 pb-10"}
            onClick={(e) => {
                e.stopPropagation();
            }}
        >
            <div className='flex flex-row items-center justify-between'>
                <h2 className='text-xl font-semibold'>Edit savings goal</h2>
                <button onClick={() => (formState === FormState.Default ? closeFn() : null)}>
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
                <div className='h-fit pt-5'>
                    <div className='h-24'>
                        <label htmlFor='name' className='block text-sm font-medium leading-6 e'>
                            Name
                        </label>
                        <div className='relative  rounded-md shadow-sm'>
                            <input
                                {...register("name", {
                                    required: "name is required",
                                })}
                                defaultValue={item.name}
                                id='name'
                                type='text'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-10 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.name ? "true" : "false"}
                            />
                        </div>
                        {errors.name && errors.name.message && <FormErrorMessage message={errors.name.message} />}
                    </div>
                    <div className='h-24'>
                        <label htmlFor='name' className='block text-sm font-medium leading-6 e'>
                            Description
                        </label>
                        <div className='relative  rounded-md shadow-sm'>
                            <input
                                {...register("description")}
                                defaultValue={item.description}
                                id='description'
                                type='text'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-10 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.description ? "true" : "false"}
                            />
                        </div>
                        {errors.description && errors.description.message && <FormErrorMessage message={errors.description.message} />}
                    </div>
                    <div className='h-24'>
                        <label htmlFor='goal' className='block text-sm font-medium leading-6 e'>
                            Goal Amount
                        </label>
                        <div className='relative  rounded-md shadow-sm'>
                            <div className='pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3'>
                                <BiPound className='h-5 w-5 text-gray-400' aria-hidden='true' />
                            </div>
                            <input
                                {...register("goal", {
                                    required: "goal is required",
                                    pattern: { value: new RegExp("^[0-9]\\d*([\\,\\.]\\d{2})?$"), message: "Invalid Goal Amount" },
                                })}
                                defaultValue={item.goal}
                                id='goal'
                                type='text'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-10 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.goal ? "true" : "false"}
                            />
                        </div>
                        {errors.goal && errors.goal.message && <FormErrorMessage message={errors.goal.message} />}
                    </div>
                    <div className='h-24'>
                        <label htmlFor='goalDate' className='block text-sm font-medium leading-6 '>
                            Date
                        </label>
                        <div className='relativerounded-md shadow-sm'>
                            <input
                                {...register("goalDate")}
                                defaultValue={moment(item.goalDate).format("yyyy-MM-DD")}
                                id='date'
                                type='date'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-10 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.goalDate ? "true" : "false"}
                            />
                        </div>
                        {errors.goalDate && errors.goalDate.message && <FormErrorMessage message={errors.goalDate.message} />}
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

export default SavingsEditGoalForm;
