import { ReactNode } from "react";
import ErrorAlert from "../components/misc/ui/pageElements/ErrorAlert";
import Header from "../components/misc/ui/pageElements/Header";

interface IMainPageLayoutProps {
    children?: ReactNode;
}

const MainPageLayout = ({ children }: IMainPageLayoutProps) => {
    return (
        <main className='container mx-auto max-w-7xl sm:px-6 lg:px-8'>
            <Header />
            {children}
            <ErrorAlert />
        </main>
    );
};

export default MainPageLayout;
