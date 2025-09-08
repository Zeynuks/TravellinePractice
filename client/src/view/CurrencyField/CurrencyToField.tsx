import {Field} from "../components/Field/Field.tsx";
import {useCurrencyToField} from "./CurrencyToField.state.ts";

export const CurrencyToField = () => (
    <Field {...useCurrencyToField()} />
);