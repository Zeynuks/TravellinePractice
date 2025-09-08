import {useExchangeRateStore} from "../exchange-rate-store";
import {useStore} from "../store";

export const useRate = () => useStore(useExchangeRateStore(), (state) => state.rates);
