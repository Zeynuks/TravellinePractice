import classes from "./Button.module.scss";
import type {useButtonState} from "./Button.state.ts";

export const ButtonView = ({
                               label,
                               onClick = () => {
                               },
                               type = "button",
                               disabled = false,
                           }: ReturnType<typeof useButtonState>) => (
    <div className={classes.field}>
        <button className={classes.button} type={type} onClick={onClick} disabled={disabled}>{label}</button>
    </div>
);