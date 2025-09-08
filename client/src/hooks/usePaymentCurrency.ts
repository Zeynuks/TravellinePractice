import {useExchangeRateStore} from "../exchange-rate-store";
import {useStore} from "../store";

export const usePaymentCurrency = () => useStore(useExchangeRateStore(), (state) => state.paymentCurrency);
