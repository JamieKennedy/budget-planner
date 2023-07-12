import { CartesianGrid, Legend, Line, LineChart, ReferenceLine, ResponsiveContainer, Tooltip, XAxis, YAxis } from "recharts";

import moment from "moment";
import { TSavings } from "../../../../../../types/Savings";

interface ISavingsChartProps {
    item: TSavings;
}

type TChartDataItem = {
    date: number;
    balance: number;
};

const SavingsChart = ({ item }: ISavingsChartProps) => {
    const earliestDate = item.savingsBalances[item.savingsBalances.length - 1].created.getTime();
    const lastDate = item.goalDate ? item.goalDate.getTime() : Date.now();

    const chartData: TChartDataItem[] = item.savingsBalances
        .map((i) => {
            return {
                date: i.created.getTime(),
                balance: i.balance,
            } as TChartDataItem;
        })
        .reverse();

    return (
        <div className='w-full h-72 col-span-3 flex flex-col items-center p-5'>
            <ResponsiveContainer width='100%' height='100%'>
                <LineChart width={700} height={300} data={chartData} margin={{ bottom: 50 }}>
                    <CartesianGrid stroke='#ccc' />
                    <XAxis
                        dataKey='date'
                        tickFormatter={(value) => moment(value).format("DD/MM/YY")}
                        domain={[earliestDate, lastDate]}
                        type='number'
                        tickCount={10}
                        interval={"preserveStart"}
                    />
                    <YAxis domain={[0, item.goal]} interval={"preserveStart"} tickCount={100} padding={{ top: 10 }} tickFormatter={(value) => `£${value}`} />
                    <Tooltip formatter={(value) => `£${value}`} labelFormatter={(value) => moment(value).format("DD/MM/YY HH:mm")} />
                    <Line type='monotone' name='Balance' dataKey='balance' stroke='#8884d8' strokeWidth={2} />
                    {/* Fake line to allow reference line to show on legend */}
                    <Line type='monotone' name='Target' dataKey='null' stroke='gray' connectNulls strokeDasharray='8' dot={false} />
                    <ReferenceLine
                        segment={[
                            { x: chartData[0].date, y: 0 },
                            { x: lastDate, y: item.goal },
                        ]}
                        ifOverflow='hidden'
                        strokeWidth={2}
                        strokeDasharray={"15 15"}
                        stroke='gray'
                    />
                    <Legend iconType='plainline' />
                </LineChart>
            </ResponsiveContainer>
        </div>
    );
};

export default SavingsChart;
