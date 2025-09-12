import type {Currency, ExchangeRate} from "./domain";

const exchangeRateStorageKey = `exchangeRateStorageKey`;
const curreniesStorageKey = `currenciesStorageKey`;

const setExchangeRate = (value: ExchangeRate) => localStorage.setItem(exchangeRateStorageKey, JSON.stringify(value));

const getExchangeRate = (): ExchangeRate | undefined => {
    const item = localStorage.getItem(exchangeRateStorageKey) ?? undefined;

    return item === undefined ? undefined : JSON.parse(item);
};

const setCurrencies = (value: Currency[]) => localStorage.setItem(curreniesStorageKey, JSON.stringify(value));

const getCurrencies = (): Currency[] | undefined => {
    const item = localStorage.getItem(curreniesStorageKey) ?? undefined;

    return item === undefined ? undefined : JSON.parse(item);
};

export const AppStorage = {getExchangeRate, setExchangeRate, getCurrencies, setCurrencies};