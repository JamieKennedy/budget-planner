import { ReactNode } from 'react';
import { BeatLoader } from 'react-spinners';
import { EMutationStatus } from '../../../types/Enum';

interface IFormSubmitProps {
    defaultStateText: string;
    enabled?: boolean;
    formState: EMutationStatus;
    className?: string;
    children?: ReactNode;
}

const FormSubmit = ({ defaultStateText, enabled = true, formState, className }: IFormSubmitProps) => {
    const disabled: boolean = !enabled || formState == 'pending';

    const buttonElement = (): JSX.Element => {
        switch (formState) {
            case 'idle':
                return <p>{defaultStateText}</p>;
            case 'pending':
                return (
                    <div className='w-full'>
                        <BeatLoader color='#ffffff' size={10} margin={0} />
                    </div>
                );
            default:
                return <p>{defaultStateText}</p>;
        }
    };

    return (
        <button type='submit' disabled={disabled} className={className}>
            {buttonElement()}
        </button>
    );
};

export default FormSubmit;
