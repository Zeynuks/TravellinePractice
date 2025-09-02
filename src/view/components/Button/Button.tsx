import {ButtonView} from "./Button.view.tsx";
import {useButtonState} from "./Button.state.ts";

export type ButtonType = "button" | "submit" | "reset"

export type ButtonProps = {
    label: string;
    onClick?: () => void;
    type?: ButtonType;
    disabled?: boolean;
};

export const Button = (props: ButtonProps) => (
    <ButtonView {...useButtonState(props)} />
);
