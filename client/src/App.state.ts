import {AppStorage} from "./AppStorage";
import {useExchangeRateStore} from "./exchange-rate-store";
import {useSubscribeStore} from "./store";
import {useCurrenciesStore} from "./currencies-store";

export const useAppState = () => {
    useSubscribeStore(useExchangeRateStore(), (value) => AppStorage.setExchangeRate(value));
    useSubscribeStore(useCurrenciesStore(), (value) => AppStorage.setCurrencies(value));
};