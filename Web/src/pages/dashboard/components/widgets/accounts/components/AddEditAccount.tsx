import { TAccount, TCreateAccount, TUpdateAccount } from "../../../../../../types/Accounts";

import { useState } from "react";
import { useForm } from "react-hook-form";
import { BiPound } from "react-icons/bi";
import { Accounts } from "../../../../../../api/Accounts";
import FormErrorMessage from "../../../../../../components/misc/ui/FormErrorMessage";
import FormSubmitButton from "../../../../../../components/misc/ui/FormSubmitButton";
import useApi from "../../../../../../hooks/useApi";
import useAppStore from "../../../../../../state/Store";
import { EFormState } from "../../../../../../types/Enum";

interface IAddEditAccountProps {
    account?: TAccount;
    closeFn: () => void;
    addAccount?: (account: TAccount) => void;
    editAccount?: (account: TAccount) => void;
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

const AddEditAccount = ({ account, closeFn, addAccount, editAccount }: IAddEditAccountProps) => {
    const [user, setAppError] = useAppStore((state) => [state.User, state.setError]);
    const [formState, setFormState] = useState<EFormState>("Default");

    const [updateAccount] = useApi<TAccount, TUpdateAccount>(Accounts.UpdateAccount);
    const [createAccount] = useApi<TAccount, TCreateAccount>(Accounts.CreateAccount);

    const {
        register,
        handleSubmit,
        setError,
        formState: { errors },
    } = useForm<TAddEditAccountFormData>({ defaultValues: defaultValues(account) });

    const onEdit = async (editData: TUpdateAccount): Promise<boolean> => {
        const [updatedAccount, updateError] = await updateAccount(editData);

        if (updateError) {
            setAppError(updateError.Message);
            return false;
        }

        if (!editAccount) {
            return false;
        }

        editAccount(updatedAccount);
        return true;
    };

    const onAdd = async (addData: TCreateAccount) => {
        const [newAccount, createAccountError] = await createAccount(addData);

        if (createAccountError) {
            setAppError(createAccountError.Message);
            return false;
        }

        if (!addAccount) {
            return false;
        }

        addAccount(newAccount);
        return true;
    };

    const onSubmit = async (formData: TAddEditAccountFormData) => {
        if (!user) {
            setFormState("Errored");
            setError("root", {
                message: "An error has occured",
            });

            return;
        }

        let result = false;

        if (account) {
            result = await onEdit({ userId: user.id, accountId: account.id, name: formData.name, balance: formData.balance });
        } else {
            result = await onAdd({ userId: user.id, name: formData.name, balance: formData.balance });
        }

        if (result) {
            closeFn();
        }
    };
    return (
        <div
            className={"border-2 border-gray-200 bg-white rounded-2xl shadow-xl overflow-hidden max-w-xl w-full h-fit flex flex-col p-5 pb-10"}
            onClick={(e) => {
                e.stopPropagation();
            }}
        >
            <div className='flex flex-row items-center justify-between'>
                <h2 className='text-xl font-semibold'>Add new account</h2>
                <button onClick={() => (formState === "Default" ? closeFn() : null)}>
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
                                id='name'
                                type='text'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-3 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.name ? "true" : "false"}
                            />
                        </div>
                        {errors.name && errors.name.message && <FormErrorMessage message={errors.name.message} />}
                    </div>
                    <div className='h-24'>
                        <label htmlFor='goal' className='block text-sm font-medium leading-6 e'>
                            Balance
                        </label>
                        <div className='relative  rounded-md shadow-sm'>
                            <div className='pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3'>
                                <BiPound className='h-5 w-5 text-gray-400' aria-hidden='true' />
                            </div>
                            <input
                                {...register("balance", {
                                    required: "Balance is required",
                                })}
                                id='goal'
                                type='text'
                                className='block w-full mb-2 rounded-md border-0 py-1.5 pl-10 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6'
                                aria-invalid={errors.balance ? "true" : "false"}
                            />
                        </div>
                        {errors.balance && errors.balance.message && <FormErrorMessage message={errors.balance.message} />}
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

export default AddEditAccount;
