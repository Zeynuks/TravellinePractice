import type {ExchangeRate} from "./domain";

const exchangeRateStorageKey = `exchangeRateStorageKey`;

const setExchangeRate = (value: ExchangeRate) => localStorage.setItem(exchangeRateStorageKey, JSON.stringify(value));

const getExchangeRate = (): ExchangeRate | undefined => {
    const item = localStorage.getItem(exchangeRateStorageKey) ?? undefined;

    return item === undefined ? undefined : JSON.parse(item);
};

export const AppStorage = {getExchangeRate, setExchangeRate};