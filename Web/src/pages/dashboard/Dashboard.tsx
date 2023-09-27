import MainPageLayout from '../../layouts/MainPageLayout';
import WidgetContainer from './components/WidgetContainer';
import AccountsWidget from './components/widgets/accounts/AccountsWidget';
import SavingsWidget from './components/widgets/savings/SavingsWidget';

const Dashboard = () => {
    return (
        <MainPageLayout>
            <section className="grid grid-cols-1 lg:grid-cols-2 gap-6 pb-10">
                <WidgetContainer className="col-span-1 lg:col-span-1">
                    <SavingsWidget />
                </WidgetContainer>
                <WidgetContainer className="col-span-1">
                    <AccountsWidget />
                </WidgetContainer>
            </section>
        </MainPageLayout>
    );
};

export default Dashboard;
