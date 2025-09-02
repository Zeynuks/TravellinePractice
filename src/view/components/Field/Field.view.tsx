import type {useFieldState} from "./Field.state.ts";
import classes from "./Field.module.scss";

export const FieldView = ({
                              id,
                              value,
                              onChange,
                              label = "",
                              placeholder = "",
                              required = false,
                          }: ReturnType<typeof useFieldState>) => (
    <div className={classes.field}>
        <label htmlFor={id} className={classes.label}>{required ? `*${label}` : label}</label>
        <input
            id={id}
            className={classes.input}
            value={value}
            onChange={onChange}
            placeholder={placeholder}
            maxLength={255}/>
    </div>
);
