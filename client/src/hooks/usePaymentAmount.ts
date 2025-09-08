import {useExchangeRateStore} from "../exchange-rate-store";
import {useStore} from "../store";

export const usePaymentAmount = () => useStore(useExchangeRateStore(), (state) => state.paymentAmount);
