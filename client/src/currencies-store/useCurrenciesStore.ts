import {useContext} from "react";
import {CurrenciesContext} from "./CurrenciesContext.tsx";

export const useCurrenciesStore = () => useContext(CurrenciesContext);
