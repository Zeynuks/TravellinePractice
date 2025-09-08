import {useExchangeRateStore} from "../exchange-rate-store";
import {useStore} from "../store";

export const usePurchasedCurrency = () => useStore(useExchangeRateStore(), (state) => state.purchasedCurrency);
