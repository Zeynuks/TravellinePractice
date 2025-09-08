import {useExchangeRateStore} from "../exchange-rate-store";
import {useStore} from "../store";
import {useCurrencyListStore} from "../currency-store";

export const useCurrencyList = () => useStore(useCurrencyListStore(), (state) => state.list);
