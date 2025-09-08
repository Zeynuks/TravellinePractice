import {useContext} from "react";
import {ExchangeRateContext} from "./ExchangeRateContext.tsx";

export const useExchangeRateStore = () => useContext(ExchangeRateContext);
