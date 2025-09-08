import {AppStorage} from "./AppStorage";
import {useExchangeRateStore} from "./exchange-rate-store";
import {useSubscribeStore} from "./store";

export const useAppState = () => {
    useSubscribeStore(useExchangeRateStore(), (value) => AppStorage.setExchangeRate(value));
};