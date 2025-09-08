import {CurrencyDropdown} from "../CurrencyDropdown.tsx";
import {useCurrencyFromDropdownState} from "./CurrencyFromDropdown.state.tsx";

export const CurrencyFromDropdown = () => (<CurrencyDropdown {...useCurrencyFromDropdownState()}/>)