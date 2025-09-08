import {RateChartView} from "./RateChart.view.tsx";
import {useRateChart} from "./RateChart.state.ts";

export const RateChart = () => <RateChartView {...useRateChart()}/>