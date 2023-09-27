import { ReactNode } from 'react';

interface IWidgetContainerProps {
    className?: string;
    children?: ReactNode;
}

const WidgetContainer = ({ children, className }: IWidgetContainerProps) => {
    return (
        <div
            className={`border-2 border-gray-200 dark:border-blueMain rounded-2xl shadow-xl ${className}`}
        >
            {children}
        </div>
    );
};

export default WidgetContainer;
