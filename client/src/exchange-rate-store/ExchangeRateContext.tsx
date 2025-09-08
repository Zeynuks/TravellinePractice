import {Store} from "../store";
import {createContext} from "react";
import {AppStorage} from "../AppStorage.ts";
import {defaultExchangeRate, type ExchangeRate} from "../domain";

export const ExchangeRateContext = createContext<Store<ExchangeRate>>(Store.create(AppStorage.getExchangeRate() ?? defaultExchangeRate));
