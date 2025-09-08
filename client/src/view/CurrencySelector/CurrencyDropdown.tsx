import {useCurrencyDropdown} from "./CurrencyDropdown.state.tsx";
import {Dropdown} from "../components/Dropdown/Dropdown.tsx";
import type {Currency} from "../../domain";

export type CurrencyDropdownProps = {
    label: string,
    current: React.ReactNode;
    setValue: (value: Currency) => void;
}

export const CurrencyDropdown = (props: CurrencyDropdownProps) => (<Dropdown {...useCurrencyDropdown(props)}/>)