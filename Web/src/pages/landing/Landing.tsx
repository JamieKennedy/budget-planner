import { SignInButton, SignUpButton } from "@clerk/clerk-react";

import { NavigationConst } from "../../constants/NavigationConst";

const Landing = () => {
    return (
        <main className='w-screen h-screen bg-slate-800 flex flex-col items-center'>
            <section className='w-full h-96 text-center py-10'>
                <h1 className='text-5xl text-white'>Budget Planner</h1>
            </section>
            <section className='flex flex-row place-content-between w-72'>
                <SignUpButton mode='modal' afterSignUpUrl={NavigationConst.PostSignUp}>
                    <div className='bg-white w-24 h-12 flex flex-col items-center justify-center'>Sign Up</div>
                </SignUpButton>
                <SignInButton mode='modal' afterSignUpUrl={NavigationConst.PostSignUp}>
                    <div className='bg-white w-24 h-12 flex flex-col items-center justify-center'>Sign In</div>
                </SignInButton>
            </section>
        </main>
    );
};

export default Landing;
