import { Bars3Icon, XMarkIcon } from '@heroicons/react/24/outline';
import { useCallback, useState } from 'react';
import { BiSolidMoon, BiSolidSun } from 'react-icons/bi';
import { Link, useLocation } from 'react-router-dom';

import { Dialog } from '@headlessui/react';
import { NavigationConst } from '../../../../constants/NavigationConst';
import { useLogout } from '../../../../hooks/useLogout';
import useAppStore from '../../../../state/Store';
import { ETheme } from '../../../../types/Enum';
import { cn } from '../../../../utils/CssUtils';
import Toggle from '../interactable/Toggle';

const navigation = [
    { name: 'Dashboard', href: NavigationConst.Dashboard },
    { name: 'Settings', href: NavigationConst.Settings },
];

const Header = () => {
    const [theme, setTheme] = useAppStore((state) => [state.Theme, state.setTheme]);

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
        <header className='bg-white dark:bg-dark'>
            <nav className='mx-auto flex max-w-7xl items-center justify-between p-6 lg:px-8 gap-10' aria-label='Global'>
                <div className='flex items-center gap-x-12'>
                    <div className='hidden lg:flex lg:gap-x-12'>
                        {navigation.map((item) => (
                            <Link
                                to={item.href}
                                className={cn('text-sm font-semibold leading-6 text-gray-900 dark:text-white hover:text-blue-600 dark:hover:text-gray-200', {
                                    'text-blue-600 dark:text-gray-200': isHighlighted(item.name),
                                })}
                                key={item.name}
                            >
                                {item.name}
                            </Link>
                        ))}
                    </div>
                </div>
                <div className='flex lg:hidden'>
                    <button type='button' className='-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-gray-900' onClick={() => setMobileMenuOpen(true)}>
                        <span className='sr-only'>Open main menu</span>
                        <Bars3Icon className='h-6 w-6 dark:text-white' aria-hidden='true' />
                    </button>
                </div>
                <div className='ml-auto hidden lg:block'>
                    <Toggle<ETheme>
                        values={['Light', 'Dark']}
                        onChange={(val) => setTheme(val)}
                        currentValue={theme}
                        icons={[<BiSolidSun className='text-yellow-500' />, <BiSolidMoon className='text-white' />]}
                        switchClassName='dark:bg-dark text-white'
                    />
                </div>
                <div className='hidden lg:flex text-gray-900 dark:text-white hover:text-blue-600'>
                    <button className='-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 ' onClick={logoutHandler}>
                        Log out
                    </button>
                </div>
            </nav>
            <Dialog
                as='div'
                className={cn('lg:hidden h-screen', {
                    dark: theme === 'Dark',
                })}
                open={mobileMenuOpen}
                onClose={setMobileMenuOpen}
            >
                <div className='fixed inset-0 z-10' />
                <Dialog.Panel className='fixed inset-y-0 right-0 z-10 w-full overflow-y-auto bg-white dark:bg-dark px-6 py-6 sm:max-w-sm sm:ring-1 sm:ring-gray-900/10 dark:sm:ring-blueMain/10'>
                    <div className='flex items-right justify-right'>
                        <button type='button' className='-m-2.5 rounded-md p-2.5 text-gray-700' onClick={() => setMobileMenuOpen(false)}>
                            <span className='sr-only'>Close menu</span>
                            <XMarkIcon className='h-6 w-6 dark:text-white' aria-hidden='true' />
                        </button>
                    </div>
                    <div className='mt-6 flex flex-col grow'>
                        <div className='-my-6 divide-y divide-gray-500/10'>
                            <div className='space-y-2 py-6'>
                                {navigation.map((item) => (
                                    <a key={item.name} href={item.href} className='-mx-3 block rounded-lg px-3 py-2 text-base font-semibold leading-7 text-gray-900 hover:text-blueMain dark:hover:text-gray-200 dark:text-white'>
                                        {item.name}
                                    </a>
                                ))}
                            </div>
                            <div className='py-6'>
                                <button className='-mx-3 block rounded-lg px-3 py-2.5 text-base font-semibold leading-7 text-gray-900 hover:text-blueMain dark:hover:text-gray-200 dark:text-white' onClick={logoutHandler}>
                                    Log out
                                </button>
                            </div>
                        </div>
                    </div>
                    <div className='absolute bottom-7'>
                        <Toggle<ETheme>
                            values={['Light', 'Dark']}
                            onChange={(val) => setTheme(val)}
                            currentValue={theme}
                            icons={[<BiSolidSun className='text-yellow-500' />, <BiSolidMoon className='text-white' />]}
                            switchClassName='dark:bg-dark text-white'
                        />
                    </div>
                </Dialog.Panel>
            </Dialog>
        </header>
    );
};

export default Header;
