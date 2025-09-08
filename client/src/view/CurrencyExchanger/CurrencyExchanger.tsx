import {CurrencyExchangerView} from "./CurrencyExchanger.view.tsx";
import {fetchExchangeRates} from "../../api/currency.ts";
import {usePaymentCurrency, usePurchasedCurrency} from "../../hooks";
import {useExchangeRateStore} from "../../exchange-rate-store";
import {useEffect} from "react";

export const CurrencyExchanger = () => {
    const exchangeRateStore = useExchangeRateStore();
    const fromCurrency = usePaymentCurrency();
    const toCurrency = usePurchasedCurrency();

    const fetchData = async () => {
        try {
            const data = await fetchExchangeRates({
                PaymentCurrency: fromCurrency.code,
                PurchasedCurrency: toCurrency.code
            });

            exchangeRateStore.set({
                ...exchangeRateStore.getSnapshot(),
                rates: data
            });
        } catch (error) {
            console.error('Error fetching exchange rates:', error);
        }
    };

    useEffect(() => {
        fetchData();

        const intervalId = setInterval(fetchData, 60000);

        return () => clearInterval(intervalId);
    }, [fromCurrency.code, toCurrency.code]);

    return  <CurrencyExchangerView/>
};