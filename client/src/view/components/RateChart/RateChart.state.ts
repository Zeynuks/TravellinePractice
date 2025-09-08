import {useRate} from "../../../hooks";

export const useRateChart = () => {
    const rates = useRate();

    const labels = rates.map(r => {
        if (r.dateTime !== undefined) {
            const date = new Date(r.dateTime);

            return `${date?.getDay()} ${date?.toLocaleString("en-EN", {month: "long"})} ${date?.getFullYear()}`
        }

        return "";
    });
    //
    // const getDateRange = (from: Date) => {
    //
    // }

    const values = rates.map(r => r.price);

    return {labels, values};
}