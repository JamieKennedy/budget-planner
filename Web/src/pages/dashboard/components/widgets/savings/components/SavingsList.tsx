import { TSavings } from "../../../../../../types/Savings";
import { TSavingsBalanceCreate } from "../../../../../../types/SavingsBalance";
import SavingsItem from "./SavingsItem";

interface ISavingsListProps {
    addBalance: (savingsId: string, newBalance: TSavingsBalanceCreate) => void;
    items: TSavings[] | null;
}

const SavingsList = ({ addBalance, items }: ISavingsListProps) => {
    return (
        <div className='flex flex-col m-5'>
            {items &&
                items.map((item) => {
                    return <SavingsItem key={item.savingsId} item={item} addBalance={addBalance} />;
                })}
        </div>
    );
};

export default SavingsList;
