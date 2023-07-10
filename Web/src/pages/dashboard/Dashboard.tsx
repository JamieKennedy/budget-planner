import MainPageLayout from "../../layouts/MainPageLayout";
import WidgetContainer from "./components/WidgetContainer";
import SavingsWidget from "./components/widgets/savings/SavingsWidget";

const Dashboard = () => {
    return (
        <MainPageLayout>
            <section className='grid grid-cols-1 lg:grid-cols-2 gap-6 '>
                <WidgetContainer className='col-span-1 lg:col-span-2'>
                    <SavingsWidget />
                </WidgetContainer>
            </section>
        </MainPageLayout>
    );
};

export default Dashboard;
