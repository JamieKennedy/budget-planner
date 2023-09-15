import { ReactNode } from 'react';
import ErrorAlert from '../components/misc/ui/pageElements/ErrorAlert';
import Header from '../components/misc/ui/pageElements/Header';
import useAppStore from '../state/Store';
import { cn } from '../utils/CssUtils';

interface IMainPageLayoutProps {
    children?: ReactNode;
}

const MainPageLayout = ({ children }: IMainPageLayoutProps) => {
    const theme = useAppStore((state) => state.Theme);

    return (
        <main
            className={cn('', {
                dark: theme === 'Dark',
            })}
        >
            <div className=" w-full min-h-screen dark:bg-dark">
                <div className="container mx-auto max-w-7xl sm:px-6 lg:px-8">
                    <Header />
                    {children}
                </div>
            </div>
            <ErrorAlert />
        </main>
    );
};

export default MainPageLayout;
