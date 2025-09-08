import {useExchangeRateStore} from "../../exchange-rate-store";
import {usePurchasedAmount} from "../../hooks/usePurchasedAmount.ts";
import {useRate} from "../../hooks";
import {parseNumber} from "../../utils.ts";

export const useCurrencyToField = () => {
    const value = usePurchasedAmount();
    const exchangeRateStore = useExchangeRateStore();
    const rates = useRate();

    const setValue = (newValue: string) => {
        const [parsedNumber, isValid] = parseNumber(newValue)

        if (isValid) {
            const price = rates?.[rates.length - 1]?.price ?? 1;
            const [amount, isValidAmount] = parseNumber((Number(parsedNumber) / (price)).toString())

            console.log(`paymentAmount: ${amount}`);
            exchangeRateStore.set({
                ...exchangeRateStore.getSnapshot(),
                purchasedAmount: parsedNumber,
                paymentAmount: isValidAmount ? amount : "",
            });
        } else {
            exchangeRateStore.set({
                ...exchangeRateStore.getSnapshot(),
                purchasedAmount: parsedNumber,
                paymentAmount: ""
            });
        }
    };

    return {value, setValue};
};
