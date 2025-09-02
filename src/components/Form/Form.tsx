import {FormView} from "./Form.view.tsx";
import type {FormEventHandler, ReactNode} from "react";


export type FormProps = {
    label: string;
    children: ReactNode;
    onSubmit?: FormEventHandler<HTMLFormElement>;
};

export const Form = (props: FormProps) => (
    <FormView {...props} />
);
