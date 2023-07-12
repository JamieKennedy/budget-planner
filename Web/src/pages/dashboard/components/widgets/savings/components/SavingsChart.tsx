import { CartesianGrid, Legend, Line, LineChart, ReferenceLine, ResponsiveContainer, Tooltip, XAxis, YAxis } from "recharts";

import moment from "moment";
import { TSavings } from "../../../../../../types/Savings";

interface ISavingsChartProps {
    item: TSavings;
}

type TChartDataItem = {
    date: number;
    balance: number;
    target: number | null;
};

const SavingsChart = ({ item }: ISavingsChartProps) => {
    const earliestDate = item.savingsBalances[item.savingsBalances.length - 1].created.getTime();
    const lastDate = item.goalDate ? item.goalDate.getTime() : Date.now();

    const getTargetForDate = (date: Date): number => {
        const gradient = item.goal / (lastDate - earliestDate);
        const yIntercept = item.goal - gradient * lastDate;

        // y = mx + c
        const result = gradient * date.getTime() + yIntercept;
        return Math.round(result * 100) / 100;
    };

    const chartData: TChartDataItem[] = item.savingsBalances
        .map((i) => {
            const dataPoint: TChartDataItem = {
                date: i.created.getTime(),
                balance: i.balance,
                target: getTargetForDate(i.created),
            };

            return dataPoint;
        })
        .reverse();

    const dateFormat = (value: Date): string => {
        return moment(value).format("DD/MM/YY");
    };
    console.log(new Date("2023/07/11").getTime());

    return (
        <div className='w-full h-72 col-span-3 flex flex-col items-center p-5'>
            <ResponsiveContainer width='100%' height='100%'>
                <LineChart width={700} height={300} data={chartData} margin={{ bottom: 50 }}>
                    <CartesianGrid stroke='#ccc' />
                    <XAxis
                        dataKey='date'
                        tickFormatter={dateFormat}
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
