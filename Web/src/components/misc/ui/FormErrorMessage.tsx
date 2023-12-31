import { FunctionComponent } from 'react';
import { BiError } from 'react-icons/bi';

interface IFormErrorMessageProps {
    message: string;
}

const FormErrorMessage: FunctionComponent<IFormErrorMessageProps> = ({ message }: IFormErrorMessageProps) => {
    return (
        <div className='inline-flex items-center text-red-600'>
            <BiError className='mr-1' />{' '}
            <p className='font-semibold'>
                <span role='alert'>{message}</span>
            </p>
        </div>
    );
};

export default FormErrorMessage;
