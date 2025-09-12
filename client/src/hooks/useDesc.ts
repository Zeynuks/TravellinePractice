import {useExchangeRateStore} from "../exchange-rate-store";
import {useStore} from "../store";

export const useDesc = () => useStore(useExchangeRateStore(), (state) => state.desc);
