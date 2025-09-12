import {Store} from "../store";
import {createContext} from "react";
import {AppStorage} from "../AppStorage.ts";
import {type Currency} from "../domain";

export const CurrenciesContext = createContext<Store<Currency[]>>(Store.create(AppStorage.getCurrencies() ?? []));
