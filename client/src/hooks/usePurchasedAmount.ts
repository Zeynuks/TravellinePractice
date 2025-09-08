import {useExchangeRateStore} from "../exchange-rate-store";
import {useStore} from "../store";

export const usePurchasedAmount = () => useStore(useExchangeRateStore(), (state) => state.purchasedAmount);
