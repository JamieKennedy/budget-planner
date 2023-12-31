import { useForm } from 'react-hook-form';
import { BiPound } from 'react-icons/bi';
import useSavings from '../../../../../../api/hooks/useSavings';
import FormErrorMessage from '../../../../../../components/misc/ui/FormErrorMessage';
import FormSubmitButton from '../../../../../../components/misc/ui/FormSubmitButton';
import { TSavingsCreate } from '../../../../../../types/Savings';

interface ISavingsAddGoalFormProps {
    closeFn: () => void;
}

const SavingsAddGoalForm = ({ closeFn }: ISavingsAddGoalFormProps) => {
    const { createSavings } = useSavings();

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<TSavingsCreate>();

    const onSubmit = (formData: TSavingsCreate) => {
        if (!formData.goalDate) {
            formData.goalDate = undefined;
        }

        createSavings.mutate(formData, {
            onSuccess: () => {
                closeFn();
            },
        });
    };

    return (
        <div
            className={'border-2 border-gray-200 dark:border-blueMain bg-white dark:bg-dark rounded-2xl shadow-xl overflow-hidden max-w-xl w-full h-fit flex flex-col p-5 pb-10 dark:text-white'}
            onClick={(e) => {
                e.stopPropagation();
            }}
        >
            <div className='flex flex-row items-center justify-between'>
                <h2 className='text-xl font-semibold'>Add new savings goal</h2>
                <button onClick={() => (createSavings.status !== 'pending' ? closeFn() : null)}>
                    <svg xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 24 24' strokeWidth={1.5} stroke='currentColor' className='w-6 h-6 hover:text-gray-500'>
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
                                {...register('name', {
                                    required: 'name is required',
                                })}
                                id='name'
                                type='text'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-3 text-gray-900 ring-1 ring-inset dark:bg-darkLigher dark:ring-transparent dark:focus:ring-blueMain ring-gray-300 placeholder:text-gray-400 focus:ring-2 dark:focus:ring-1 dark:text-white focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.name ? 'true' : 'false'}
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
                                {...register('description')}
                                id='description'
                                type='text'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-3 text-gray-900 ring-1 ring-inset dark:bg-darkLigher dark:ring-transparent dark:focus:ring-blueMain ring-gray-300 placeholder:text-gray-400 focus:ring-2 dark:focus:ring-1 dark:text-white focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.description ? 'true' : 'false'}
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
                                {...register('goal', {
                                    required: 'goal is required',
                                    pattern: {
                                        value: new RegExp('^[0-9]\\d*([\\,\\.]\\d{2})?$'),
                                        message: 'Invalid Goal Amount',
                                    },
                                })}
                                id='goal'
                                type='text'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-10 text-gray-900 ring-1 ring-inset dark:bg-darkLigher dark:ring-transparent dark:focus:ring-blueMain ring-gray-300 placeholder:text-gray-400 focus:ring-2 dark:focus:ring-1 dark:text-white focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.goal ? 'true' : 'false'}
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
                                {...register('goalDate')}
                                id='date'
                                type='date'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-10 text-gray-900 ring-1 ring-inset dark:bg-darkLigher dark:ring-transparent dark:focus:ring-blueMain ring-gray-300 placeholder:text-gray-400 focus:ring-2 dark:focus:ring-1 dark:text-white focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.goalDate ? 'true' : 'false'}
                            />
                        </div>
                        {errors.goalDate && errors.goalDate.message && <FormErrorMessage message={errors.goalDate.message} />}
                    </div>
                    <div className='mt-3'>
                        <FormSubmitButton
                            defaultStateText='Save'
                            formState={createSavings.status}
                            className='flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600 mb-2'
                        />
                    </div>
                </div>
            </form>
        </div>
    );
};

export default SavingsAddGoalForm;
