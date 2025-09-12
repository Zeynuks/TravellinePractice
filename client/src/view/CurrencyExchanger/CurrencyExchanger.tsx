import {CurrencyExchangerView} from "./CurrencyExchanger.view.tsx";
import {fetchExchangeRates} from "../../api/currency.ts";
import {usePaymentCurrency, usePurchasedCurrency} from "../../hooks";
import {useExchangeRateStore} from "../../exchange-rate-store";
import {useEffect} from "react";

export const CurrencyExchanger = () => {
    const exchangeRateStore = useExchangeRateStore();
    const fromCurrency = usePaymentCurrency();
    const toCurrency = usePurchasedCurrency();

    const fetchRates = async () => {
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

    const fetchCurrencies = async () => {
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
        fetchRates();

        const intervalId = setInterval(fetchRates, 60000);

        return () => clearInterval(intervalId);
    }, [fromCurrency.code, toCurrency.code]);

    useEffect(() => {
        fetchCurrencies();

        const intervalId = setInterval(fetchCurrencies, 6000000);

        return () => clearInterval(intervalId);
    }, []);

    return  <CurrencyExchangerView/>
};