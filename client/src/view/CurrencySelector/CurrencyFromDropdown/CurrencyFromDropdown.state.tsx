import {CurrencyItem} from "../CurrencyItem.tsx";
import {usePaymentCurrency} from "../../../hooks";
import type {Currency} from "../../../domain";
import {useExchangeRateStore} from "../../../exchange-rate-store";

export const useCurrencyFromDropdownState = () => {
    const label = "From";
    const current = <CurrencyItem currency={usePaymentCurrency()}/>;
    const exchangeRateStore = useExchangeRateStore();

    const setValue = (newValue: Currency) => exchangeRateStore.set({
        ...exchangeRateStore.getSnapshot(),
        paymentCurrency: newValue,
        desc: false
    });

    return {label, current, setValue};
}