import {CurrencyDropdown} from "../CurrencyDropdown.tsx";
import {useCurrencyToDropdownState} from "./CurrencyToDropdown.state.tsx";

export const CurrencyToDropdown = () => (<CurrencyDropdown {...useCurrencyToDropdownState()}/>)