import type {Rate} from "./Rate.ts";
import type {Currency} from "./Currency.ts";

export type ExchangeRate = {
    paymentCurrency: Currency;
    paymentAmount: string;
    purchasedCurrency: Currency;
    purchasedAmount: string;
    desc: boolean;
    rates: Rate[];
}