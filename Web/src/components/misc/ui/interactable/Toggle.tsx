import { ReactNode, useState } from 'react';

import { cn } from '../../../../utils/CssUtils';

interface IToggleProps<T> {
    values: [T, T];
    currentValue: T;
    onChange: (newVal: T) => void;
    icons?: [ReactNode, ReactNode];
    containerclassName?: string;
    switchClassName?: string;
}

const Toggle = <T,>({ values, currentValue, onChange, icons, containerclassName, switchClassName }: IToggleProps<T>) => {
    const [position, setPosition] = useState<0 | 1>(values.indexOf(currentValue) === 0 ? 0 : 1);

    const onClick = () => {
        setPosition((current) => (current === 0 ? 1 : 0));
        onChange(values[position === 0 ? 1 : 0]);
    };

    return (
        <div className={cn('flex flex-row items-center w-14 h-7 rounded-full bg-gray-300 relative shadow-inner', containerclassName)} onClick={() => onClick()}>
            <div
                className={cn('w-7 h-7 rounded-full bg-white absolute transition-left border-2 border-gray-300 flex flex-row items-center justify-center', switchClassName, {
                    'left-0': position === 0,
                    'left-1/2': position === 1,
                })}
            >
                {icons && icons[position]}
            </div>
        </div>
    );
};

export default Toggle;
