import {RateInfoView} from "./RateInfo.view.tsx";
import {useRateInfoState} from "./RateInfo.state.ts";

export type RateInfoProps = {
    currentRate: number;
    todayChange: number;
    todayChangePercent: number;
}

export const RateInfo = () => <RateInfoView {...useRateInfoState()}/>