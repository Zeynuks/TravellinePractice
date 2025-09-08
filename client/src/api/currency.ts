import {fetchJson, toQueryString} from "./http";
import type {Currency, Rate} from "../domain";

type PriceChangeResponse = {
    purchasedCurrencyCode: string;
    paymentCurrencyCode: string;
    price: number;
    dateTime: string;
};

export type GetExchangeRatesRequest = {
    PurchasedCurrency: string;
    PaymentCurrency: string;
    FromDateTime?: string;
    ToDateTime?: string;
};

export async function fetchAllCurrencies(): Promise<Currency[]> {
    const response = await fetchJson<Currency[]>("/currency");

    return response.map((currency) => ({
        code: currency.code,
        name: currency.name,
        description: currency.description,
        symbol: currency.symbol,
    }));
}

export async function fetchCurrencyByCode(currencyCode: string): Promise<Currency> {
    const currency = await fetchJson<Currency>(
        `/currency/${encodeURIComponent(currencyCode)}`
    );

    return {
        code: currency.code,
        name: currency.name,
        description: currency.description,
        symbol: currency.symbol,
    };
}

export async function fetchExchangeRates(request: GetExchangeRatesRequest): Promise<Rate[]> {
    const formatDate = (date: Date): string => {
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();
        return `${day}-${month}-${year}`;
    };

    const fromDateTime = request.FromDateTime
        ? new Date(request.FromDateTime)
        : new Date().setFullYear(new Date().getFullYear() - 1);

    const toDateTime = request.ToDateTime ? new Date(request.ToDateTime) : new Date();

    const response = await fetchJson<PriceChangeResponse[]>(
        `/prices${toQueryString({
            ...request,
            fromDateTime: formatDate(new Date(fromDateTime)),
            toDateTime: formatDate(new Date(toDateTime))
        })}`
    );


    return response.map((priceChange) => ({
        price: priceChange.price,
        dateTime: priceChange.dateTime ? new Date(priceChange.dateTime) : undefined,
    }))
}
