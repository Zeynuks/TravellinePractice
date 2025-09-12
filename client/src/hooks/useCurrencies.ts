import {useStore} from "../store";
import {useCurrenciesStore} from "../currencies-store";

export const useCurrencies = () => useStore(useCurrenciesStore(), (state) => state);
