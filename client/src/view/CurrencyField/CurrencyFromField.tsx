import {Field} from "../components/Field/Field.tsx";
import {useCurrencyFromField} from "./CurrencyFromField.state.ts";

export const CurrencyFromField = () => (
    <Field {...useCurrencyFromField()} />
);