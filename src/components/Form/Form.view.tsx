import classes from "./Form.module.scss";
import type {FormProps} from "./Form.tsx";

export const FormView = ({
                             label,
                             children,
                             onSubmit = () => {
                             }
                         }: FormProps) => (
    <form className={classes.form} onSubmit={onSubmit}>
        <h1 className={classes.title}>{label}</h1>
        {children}
    </form>
);
