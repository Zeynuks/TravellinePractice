import type {ChartOptions} from "chart.js";
import {CategoryScale, Chart as ChartJS, Legend, LinearScale, LineElement, PointElement, Tooltip,} from "chart.js";
import {Line} from "react-chartjs-2";
import type {useRateChart} from "./RateChart.state.ts";

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Tooltip, Legend);

const options: ChartOptions<"line"> = {
    responsive: true,
    maintainAspectRatio: false,
    interaction: {
        mode: "nearest",
        intersect: false,
    },
    plugins: {
        legend: {display: false},
        title: {display: false},
        tooltip: {
            enabled: true,
            displayColors: false,
            backgroundColor: "#FFFFFF",
            borderColor: "rgba(160, 160, 161, 0.5)",
            bodyColor: "#414142",
            titleColor: "#414142",
        },
    },
    elements: {
        line: {
            borderColor: "#D4A1D0",
            tension: 0.4,
            borderWidth: 2,
        },
        point: {
            radius: 0,
            hoverRadius: 6,
            hitRadius: 20,
            backgroundColor: "#D4A1D0",
            borderColor: "#FFFFFF",
            borderWidth: 2,
        },
    },
    scales: {
        x: {
            offset: true,
            display: false,
            ticks: {display: false},
            border: {display: false}
        },
        y: {
            offset: true,
            display: false,
            ticks: {display: false},
            border: {display: false}
        },
    },
    layout: {
        padding: 2,
    },
};

export const RateChartView = ({labels, values}: ReturnType<typeof useRateChart>) => {
    const data = {
        labels,
        datasets: [
            {
                data: values,
            },
        ],
    };

    return (
        <div style={{height: 150, width: "100%", position: "relative"}}>
            <Line data={data} options={options}/>
        </div>
    )
};
