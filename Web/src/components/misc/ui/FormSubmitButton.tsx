import { ReactNode } from 'react';
import { BeatLoader } from 'react-spinners';
import { EFormState } from '../../../types/Enum';

interface IFormSubmitProps {
    defaultStateText: string;
    enabled?: boolean;
    formState: EFormState;
    className?: string;
    children?: ReactNode;
}

const FormSubmit = ({ defaultStateText, enabled = true, formState, className }: IFormSubmitProps) => {
    const disabled: boolean = !enabled || formState !== 'Default';

    const buttonElement = (): JSX.Element => {
        switch (formState) {
            case 'Default':
                return <p>{defaultStateText}</p>;
            case 'Pending':
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
