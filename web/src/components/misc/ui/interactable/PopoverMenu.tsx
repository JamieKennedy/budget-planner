import { Popover, Transition } from '@headlessui/react';
import { Fragment, ReactNode } from 'react';

import { cn } from '../../../../utils/CssUtils';

interface IPopoverMenuProps {
    button: ReactNode;
    children?: ReactNode;
}

const PopoverMenu = ({ button, children }: IPopoverMenuProps) => {
    return (
        <Popover className='relative'>
            {() => (
                <>
                    <Popover.Button className={cn('flex rounded-full border-none text-sm ring-2 ring-transparent')}>{button}</Popover.Button>
                    <Transition
                        as={Fragment}
                        enter='transition ease-out duration-200'
                        enterFrom='opacity-0 translate-y-0'
                        enterTo='opacity-100 translate-y-1'
                        leave='transition ease-in duration-150'
                        leaveFrom='opacity-100 translate-y-1'
                        leaveTo='opacity-0 translate-y-0'
                    >
                        <Popover.Panel className={cn('absolute z-20 min-w-max bg-slate-700 rounded-lg text-placeholder-text-dark shadow-md drop-shadow-md right-0 border border-blueMain overflow-hidden')}>{children}</Popover.Panel>
                    </Transition>
                </>
            )}
        </Popover>
    );
};

export default PopoverMenu;
