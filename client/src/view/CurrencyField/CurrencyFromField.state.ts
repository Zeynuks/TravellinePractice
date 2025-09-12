import {useExchangeRateStore} from "../../exchange-rate-store";
import {usePaymentAmount, useRate} from "../../hooks";
import {useEffect} from "react";
import {useDesc} from "../../hooks/useDesc.ts";

export const useCurrencyFromField = () => {
    const value = usePaymentAmount();
    const desc = useDesc();
    const exchangeRateStore = useExchangeRateStore();
    const rates = useRate();

    const setValue = (newValue: string) => {
        const price = rates?.[rates.length - 1]?.price ?? 1;

        exchangeRateStore.set({
            ...exchangeRateStore.getSnapshot(),
            paymentAmount: newValue,
            purchasedAmount: (Math.round(Number(newValue) * price * 100) / 100).toString()

        });
    };

    useEffect(() => {
       if (!desc) {
           const price = rates?.[rates.length - 1]?.price ?? 1;

           exchangeRateStore.set({
               ...exchangeRateStore.getSnapshot(),
               purchasedAmount: (Math.round(Number(value) * price * 100) / 100).toString()
           });
       }
    }, [desc, rates])

    return {value, setValue};
};
