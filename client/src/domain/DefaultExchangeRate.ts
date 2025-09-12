import type {ExchangeRate} from "./ExchangeRate.ts";

export const defaultExchangeRate: ExchangeRate = {
    paymentCurrency: {
        code: "RUB",
        name: "Russian ruble",
        description: "Russia's currency; sensitive to energy exports and sanctions.",
        symbol: "₽"
    },
    paymentAmount: "",
    purchasedCurrency: {
        code: "USD",
        name: "United States dollar",
        description: "World's primary reserve currency; widely used in trade and finance.",
        symbol: "$"
    },
    purchasedAmount: "",
    desc: true,
    rates: []
}