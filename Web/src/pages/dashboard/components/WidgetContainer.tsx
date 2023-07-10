import { ReactNode } from "react";

interface IWidgetContainerProps {
    className?: string;
    children?: ReactNode;
}

const WidgetContainer = ({ children, className }: IWidgetContainerProps) => {
    return <div className={`border-2 border-gray-200 rounded-2xl shadow-xl overflow-hidden ${className}`}>{children}</div>;
};

export default WidgetContainer;