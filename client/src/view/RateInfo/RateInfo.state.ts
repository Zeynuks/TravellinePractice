import {useRate} from "../../hooks";
export const useRateInfoState = () => {
    const rate = useRate();

    const format = (value: number, fix: number) => Number(value.toFixed(fix));

    const last = rate[rate.length - 1]?.price ?? 1;
    const prev = rate[rate.length - 2]?.price ?? 1;

    const currentRate = format(last, 4);
    const todayChange = format(last - prev, 4);
    const todayChangePercent = format(((last - prev)) * 100, 2);

    return { currentRate, todayChange, todayChangePercent };
};
