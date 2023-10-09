import { TAccount, TCreateAccount, TUpdateAccount } from '../../../../../../types/Accounts';

import { useForm } from 'react-hook-form';
import { BiPound } from 'react-icons/bi';
import useAccount from '../../../../../../api/hooks/useAccount';
import FormErrorMessage from '../../../../../../components/misc/ui/FormErrorMessage';
import FormSubmitButton from '../../../../../../components/misc/ui/FormSubmitButton';
import useAppStore from '../../../../../../state/Store';

interface IAddEditAccountProps {
    account?: TAccount;
    closeFn: () => void;
}

type TAddEditAccountFormData = {
    name: string;
    balance: number;
};

const defaultValues = (account?: TAccount) => {
    if (account) {
        return {
            name: account.name,
            balance: account.balance,
        };
    }
};

const AddEditAccount = ({ account, closeFn }: IAddEditAccountProps) => {
    const setAppError = useAppStore((state) => state.setError);

    const { updateAccount, createAccount } = useAccount();

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<TAddEditAccountFormData>({
        defaultValues: defaultValues(account),
    });

    const onEdit = async (editData: TUpdateAccount) => {
        updateAccount.mutate(editData, {
            onSuccess: () => {
                closeFn();
            },
            onError: (error) => {
                setAppError(error.Message);
            },
        });
    };

    const onAdd = async (addData: TCreateAccount) => {
        createAccount.mutate(addData, {
            onSuccess: () => {
                closeFn();
            },
            onError: (error) => {
                setAppError(error.Message);
            },
        });
    };

    const onSubmit = async (formData: TAddEditAccountFormData) => {
        if (account) {
            onEdit({
                accountId: account.id,
                name: formData.name,
                balance: formData.balance,
            });
        } else {
            onAdd({
                name: formData.name,
                balance: formData.balance,
            });
        }
    };

    const status = account ? updateAccount.status : createAccount.status;

    return (
        <div
            className={'border-2 border-gray-200 dark:border-blueMain bg-white rounded-2xl shadow-xl overflow-hidden max-w-xl w-full h-fit flex flex-col p-5 pb-10 dark:bg-dark'}
            onClick={(e) => {
                e.stopPropagation();
            }}
        >
            <div className='flex flex-row items-center justify-between dark:text-white'>
                <h2 className='text-xl font-semibold'>Add new account</h2>
                <button onClick={() => (status === 'idle' ? closeFn() : null)}>
                    <svg xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 24 24' strokeWidth={1.5} stroke='currentColor' className='w-6 h-6 hover:text-gray-500'>
                        <path strokeLinecap='round' strokeLinejoin='round' d='M6 18L18 6M6 6l12 12' />
                    </svg>
                </button>
            </div>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className='h-fit pt-5'>
                    <div className='h-24'>
                        <label htmlFor='name' className='block text-sm font-medium leading-6 dark:text-white'>
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
                        <label htmlFor='goal' className='block text-sm font-medium leading-6 dark:text-white'>
                            Balance
                        </label>
                        <div className='relative  rounded-md shadow-sm'>
                            <div className='pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3'>
                                <BiPound className='h-5 w-5 text-gray-400' aria-hidden='true' />
                            </div>
                            <input
                                {...register('balance', {
                                    required: 'Balance is required',
                                })}
                                id='goal'
                                type='text'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-10 text-gray-900 ring-1 ring-inset dark:bg-darkLigher dark:ring-transparent dark:focus:ring-blueMain ring-gray-300 placeholder:text-gray-400 focus:ring-2 dark:focus:ring-1 dark:text-white focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.balance ? 'true' : 'false'}
                            />
                        </div>
                        {errors.balance && errors.balance.message && <FormErrorMessage message={errors.balance.message} />}
                    </div>
                    <div className='mt-3'>
                        <FormSubmitButton
                            defaultStateText='Save'
                            formState={status}
                            className='flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600 mb-2'
                        />
                    </div>
                </div>
            </form>
        </div>
    );
};

export default AddEditAccount;
