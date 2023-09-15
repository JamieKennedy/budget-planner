import { ReactNode } from 'react';

interface IModalProps {
    children?: ReactNode;
    closeFn: () => void;
}

const Modal = ({ children, closeFn }: IModalProps) => {
    return (
        <div
            className=" fixed min-w-screen min-h-screen w-full h-full bg-black/25 top-0 right-0 flex flex-row items-center justify-center z-50"
            onClick={() => closeFn()}
        >
            {children}
        </div>
    );
};

export default Modal;
