import {CurrencyItem} from "../CurrencyItem.tsx";
import {usePurchasedCurrency} from "../../../hooks";
import type {Currency} from "../../../domain";
import {useExchangeRateStore} from "../../../exchange-rate-store";

export const useCurrencyToDropdownState = () => {
    const label = "To";
    const current = <CurrencyItem currency={usePurchasedCurrency()}/>;

    const exchangeRateStore = useExchangeRateStore();
    const setValue = (newValue: Currency) => exchangeRateStore.set({
        ...exchangeRateStore.getSnapshot(),
        purchasedCurrency: newValue
    });

    return {label, current, setValue};
}