import { CartesianGrid, Legend, Line, LineChart, ReferenceLine, ResponsiveContainer, Tooltip, XAxis, YAxis } from 'recharts';

import moment from 'moment';
import useAppStore from '../../../../../../state/Store';
import { TSavings } from '../../../../../../types/Savings';

interface ISavingsChartProps {
    item: TSavings;
}

type TChartDataItem = {
    date: number;
    balance: number;
};

const SavingsChart = ({ item }: ISavingsChartProps) => {
    const theme = useAppStore((state) => state.Theme);

    var earliestDate = item.savingsBalances[item.savingsBalances.length - 1].created.getTime();
    var lastDate: number = item.goalDate ? item.goalDate.getTime() : Date.now();

    if (moment(earliestDate).isSame(moment(lastDate), 'day')) {
        earliestDate = moment(earliestDate).add(-1, 'day').toDate().getTime();
        lastDate = moment(lastDate).add(1, 'day').toDate().getTime();
    }

    const chartData: TChartDataItem[] = item.savingsBalances
        .map((i) => {
            return {
                date: i.created.getTime(),
                balance: i.balance,
            } as TChartDataItem;
        })
        .reverse();

    return (
        <div className='w-full h-72 col-span-3 flex flex-col items-center pt-5'>
            <ResponsiveContainer width='100%' height='100%'>
                <LineChart width={700} height={300} data={chartData} margin={{ bottom: 10 }}>
                    <CartesianGrid stroke='#ccc' />
                    <XAxis
                        dataKey='date'
                        tickFormatter={(value) => moment(value).format('DD/MM/YY')}
                        domain={[earliestDate, lastDate]}
                        type='number'
                        tickCount={10}
                        interval={'equidistantPreserveStart'}
                        tick={{ fill: theme === 'Dark' ? 'white' : undefined }}
                    />
                    <YAxis domain={[0, item.goal]} interval={'preserveStart'} tickCount={100} padding={{ top: 10 }} tickFormatter={(value) => `£${value}`} tick={{ fill: theme === 'Dark' ? 'white' : undefined }} />
                    <Tooltip
                        formatter={(value) => `£${value}`}
                        labelFormatter={(value) => moment(value).format('DD/MM/YY HH:mm')}
                        itemStyle={{
                            backgroundColor: theme === 'Dark' ? '#1E2430' : undefined,
                        }}
                        contentStyle={{
                            backgroundColor: theme === 'Dark' ? '#1E2430' : undefined,
                        }}
                    />
                    <Line type='bump' name='Balance' dataKey='balance' stroke='#8884d8' strokeWidth={2} />
                    {/* Fake line to allow reference line to show on legend */}
                    <Line type='basis' name='Target' dataKey='null' stroke='gray' connectNulls strokeDasharray='8' dot={false} />
                    <ReferenceLine
                        segment={[
                            { x: chartData[0].date, y: 0 },
                            { x: lastDate, y: item.goal },
                        ]}
                        ifOverflow='hidden'
                        strokeWidth={2}
                        strokeDasharray={'15 15'}
                        stroke='gray'
                    />
                    <Legend iconType='plainline' />
                </LineChart>
            </ResponsiveContainer>
        </div>
    );
};

export default SavingsChart;
