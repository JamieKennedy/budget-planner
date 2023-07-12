import { ReactNode } from "react";

interface IModalProps {
    children?: ReactNode;
    closeFn: () => void;
}

const Modal = ({ children, closeFn }: IModalProps) => {
    return (
        <div className='w-screen h-screen bg-black/25 absolute top-0 right-0 flex flex-row items-center justify-center z-50' onClick={() => closeFn()}>
            {children}
        </div>
    );
};

export default Modal;
