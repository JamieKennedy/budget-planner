interface IProgressBarProps {
    percent: number;
}

const ProgressBar = ({ percent }: IProgressBarProps) => {
    const boundPercent = Math.max(Math.min(percent, 100), 0);

    const colorProp = 'bg-blue-600';

    const width: string = boundPercent + '%';

    return (
        <div className='hidden md:block h-2 w-36 rounded-full bg-gray-200 overflow-hidden'>
            <div className={`h-full ${colorProp} transition-width ease-in-out duration-300`} style={{ width: `${width}` }}></div>
        </div>
    );
};

export default ProgressBar;
