import { Bars3Icon, XMarkIcon } from "@heroicons/react/24/outline";
import { useCallback, useState } from "react";
import { Link, useLocation } from "react-router-dom";

import { Dialog } from "@headlessui/react";
import { NavigationConst } from "../../../../constants/NavigationConst";
import { useLogout } from "../../../../hooks/useLogout";
import { cn } from "../../../../utils/CssUtils";

const navigation = [
    { name: "Dashboard", href: NavigationConst.Dashboard },
    { name: "Settings", href: NavigationConst.Settings },
];

const Header = () => {
    const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
    const logout = useLogout();
    const location = useLocation();

    const isHighlighted = (pageName: string): boolean => {
        console.log(pageName, location.pathname);
        return location.pathname.toLowerCase().includes(pageName.toLowerCase());
    };

    const logoutHandler = useCallback(() => {
        logout();
    }, [logout]);

    return (
        <header className='bg-white'>
            <nav className='mx-auto flex max-w-7xl items-center justify-between p-6 lg:px-8' aria-label='Global'>
                <div className='flex items-center gap-x-12'>
                    <div className='hidden lg:flex lg:gap-x-12'>
                        {navigation.map((item) => (
                            <Link
                                to={item.href}
                                className={cn("text-sm font-semibold leading-6 text-gray-900 hover:text-blue-600", {
                                    "text-blue-600": isHighlighted(item.name),
                                })}
                                key={item.name}
                            >
                                {item.name}
                            </Link>
                        ))}
                    </div>
                </div>
                <div className='flex lg:hidden'>
                    <button
                        type='button'
                        className='-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-gray-900'
                        onClick={() => setMobileMenuOpen(true)}
                    >
                        <span className='sr-only'>Open main menu</span>
                        <Bars3Icon className='h-6 w-6' aria-hidden='true' />
                    </button>
                </div>
                <div className='hidden lg:flex text-gray-900 hover:text-blue-600'>
                    <button className='-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 ' onClick={logoutHandler}>
                        Log out
                    </button>
                </div>
            </nav>
            <Dialog as='div' className='lg:hidden' open={mobileMenuOpen} onClose={setMobileMenuOpen}>
                <div className='fixed inset-0 z-10' />
                <Dialog.Panel className='fixed inset-y-0 right-0 z-10 w-full overflow-y-auto bg-white px-6 py-6 sm:max-w-sm sm:ring-1 sm:ring-gray-900/10'>
                    <div className='flex items-right justify-right'>
                        <button type='button' className='-m-2.5 rounded-md p-2.5 text-gray-700' onClick={() => setMobileMenuOpen(false)}>
                            <span className='sr-only'>Close menu</span>
                            <XMarkIcon className='h-6 w-6' aria-hidden='true' />
                        </button>
                    </div>
                    <div className='mt-6 flow-root'>
                        <div className='-my-6 divide-y divide-gray-500/10'>
                            <div className='space-y-2 py-6'>
                                {navigation.map((item) => (
                                    <a
                                        key={item.name}
                                        href={item.href}
                                        className='-mx-3 block rounded-lg px-3 py-2 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50'
                                    >
                                        {item.name}
                                    </a>
                                ))}
                            </div>
                            <div className='py-6'>
                                <button
                                    className='-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-gray-900 hover:bg-gray-50'
                                    onClick={logoutHandler}
                                >
                                    Log out
                                </button>
                            </div>
                        </div>
                    </div>
                </Dialog.Panel>
            </Dialog>
        </header>
    );
};

export default Header;
