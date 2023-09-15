import { useCallback, useState } from "react";

import { useForm } from "react-hook-form";
import { useNavigate } from "react-router";
import { Authentication } from "../../api/Auth";
import { User } from "../../api/User";
import FormErrorMessage from "../../components/misc/ui/FormErrorMessage";
import FormSubmitButton from "../../components/misc/ui/FormSubmitButton";
import { NavigationConst } from "../../constants/NavigationConst";
import useApi from "../../hooks/useApi";
import useAuth from "../../hooks/useAuth";
import useAppStore from "../../state/Store";
import { TAuthorizeRequest } from "../../types/Api";
import { FormState } from "../../types/Enum";
import { TUser } from "../../types/User";
import { AuthUtils } from "../../utils/AuthUtils";

const Login = () => {
    const [formState, setFormState] = useState<FormState>(FormState.Default);
    const [loginError, setLoginError] = useState<string | null>(null);
    const [login] = useApi<string, TAuthorizeRequest>(Authentication.Login, false, true);
    const [getUser] = useApi<TUser, string>(User.GetUserById, true);
    const [, setAccessToken] = useAuth();
    const [setUser] = useAppStore((appState) => [appState.setUser]);
    const navigate = useNavigate();

    const onSubmit = useCallback(
        async (data: TAuthorizeRequest) => {
            setFormState(FormState.Pending);

            const [accessToken, loginError] = await login({
                email: data.email,
                password: data.password,
                keepLoggedIn: data.keepLoggedIn,
            });

            console.log(accessToken, loginError);

            if (loginError) {
                // Default to an error occured message
                let errorMessage = "An error has occured, please try again";

                if (loginError.StatusCode && loginError.StatusCode === 401) {
                    // if reponse is unauthorised, change error message to
                    // incorrect details
                    errorMessage = "Incorrect details";
                }

                setLoginError(errorMessage);
                setFormState(FormState.Default);
                return;
            }

            setAccessToken(accessToken);

            const userId = AuthUtils.getTokenPayload(accessToken).Id;

            const [user, userError] = await getUser(userId);

            if (userError) {
                setLoginError("Unable to retrieve the user");
                setFormState(FormState.Default);
                return;
            }

            setUser(user);
            setFormState(FormState.Default);
            navigate(NavigationConst.Dashboard);
        },
        [getUser, login, navigate, setAccessToken, setUser]
    );

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<TAuthorizeRequest>();

    return (
        <div className='flex min-h-screen flex-1 bg-gray-900'>
            <div className='flex flex-1 flex-col justify-center px-4 py-12 sm:px-6 lg:flex-none lg:px-20 xl:px-24'>
                <div className='mx-auto w-full max-w-sm lg:w-96'>
                    <div>
                        <h2 className='mt-8 text-2xl font-bold leading-9 tracking-tight text-white'>Budget Planner</h2>
                    </div>

                    <div className='mt-10 flex flex-col justify-between'>
                        <div>
                            <form onSubmit={handleSubmit(onSubmit)} className='space-y-6'>
                                <div className='h-24'>
                                    <label htmlFor='email' className='block text-sm font-medium leading-6 text-white'>
                                        Email address
                                    </label>
                                    <div className='mt-2'>
                                        <input
                                            {...register("email", { required: "Email is required", pattern: { value: /[@]/g, message: "Invalid Email" } })}
                                            id='email'
                                            type='email'
                                            autoComplete='email'
                                            className='block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6 mb-2'
                                            aria-invalid={errors.email ? "true" : "false"}
                                        />
                                        {errors.email && errors.email.message && <FormErrorMessage message={errors.email.message} />}
                                    </div>
                                </div>

                                <div className='h-24'>
                                    <label htmlFor='password' className='block text-sm font-medium leading-6 text-white'>
                                        Password
                                    </label>
                                    <div className='mt-2'>
                                        <input
                                            id='password'
                                            type={"password"}
                                            {...register("password", { required: "Password is required" })}
                                            aria-invalid={errors.password ? "true" : "false"}
                                            className='block w-full rounded-md border-0 py-1.5 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6 mb-2'
                                        />
                                        {errors.password && errors.password.message && <FormErrorMessage message={errors.password.message} />}
                                    </div>
                                </div>

                                <div className='flex items-center justify-between'>
                                    <div className='flex items-center'>
                                        <input
                                            id='keepLoggedIn'
                                            type='checkbox'
                                            {...register("keepLoggedIn")}
                                            className='h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-600'
                                        />
                                        <label htmlFor='remember-me' className='ml-3 block text-sm leading-6 text-white'>
                                            Remember me
                                        </label>
                                    </div>
                                </div>

                                <div className='h-24'>
                                    <FormSubmitButton
                                        defaultStateText='Log In'
                                        formState={formState}
                                        className='flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600 mb-2'
                                    />
                                    {loginError && (
                                        <div className='flex justify-center'>
                                            <FormErrorMessage message={loginError} />
                                        </div>
                                    )}
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
            <div className='relative hidden w-0 flex-1 lg:block'>
                <img
                    className='absolute inset-0 h-full w-full object-cover'
                    src='https://images.unsplash.com/photo-1554224155-6726b3ff858f?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=2022&q=80'
                    alt=''
                />
            </div>
        </div>
    );
};

export default Login;
