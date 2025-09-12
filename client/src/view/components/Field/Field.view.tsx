import type {useFieldState} from "./Field.state";
import classes from "./Field.module.scss";

export const FieldView = ({
                              value,
                              onChange
                          }: ReturnType<typeof useFieldState>) => {

    return (
        <div className={classes.field}>
            <input
                className={classes.input}
                type="number"
                value={value}
                onChange={(e) => onChange(e)}
                style={{
                    fontSize: `clamp(0.7em, ${1 - (value.toString().length - 5) * 0.1}em, 1em)`,
                }}
            />
        </div>
    );
};
