import {useFieldState} from "./Field.state.ts";
import {FieldView} from "./Field.view.tsx";

export type FieldProps = {
    id: string;
    value: string;
    setValue: (value: string) => void;
    label?: string;
    placeholder?: string;
    required?: boolean;
};

export const Field = (props: FieldProps) => (
    <FieldView {...useFieldState(props)} />
);
