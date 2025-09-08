import {usePaymentAmount} from "../../hooks/usePaymentAmount.ts";
import {useExchangeRateStore} from "../../exchange-rate-store";
import {useRate} from "../../hooks";
import {parseNumber} from "../../utils.ts";

export const useCurrencyFromField = () => {
    const value = usePaymentAmount();
    const exchangeRateStore = useExchangeRateStore();
    const rates = useRate();

    const setValue = (newValue: string) => {
        const [parsedNumber, isValid] = parseNumber(newValue)

        if (isValid) {
            const price = rates?.[rates.length - 1]?.price ?? 1;
            const [amount, isValidAmount] = parseNumber((Number(parsedNumber) * (price)).toString())

            console.log(`purchasedAmount: ${amount}`);
            exchangeRateStore.set({
                ...exchangeRateStore.getSnapshot(),
                paymentAmount: parsedNumber,
                purchasedAmount: isValidAmount ? amount : "",
            });
        } else {
            exchangeRateStore.set({
                ...exchangeRateStore.getSnapshot(),
                paymentAmount: parsedNumber,
                purchasedAmount: ""
            });
        }
    };

    return {value, setValue};
};
