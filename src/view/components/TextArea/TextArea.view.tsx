import classes from "./TextArea.module.scss";
import type {useTextAreaState} from "./TextArea.state.ts";
import {useRef} from "react";
import {useAutosize} from "../../../hooks/useAutosize.ts";

export const TextAreaView = ({
                                 id,
                                 label,
                                 value,
                                 onChange,
                                 maxLength = 3000,
                                 placeholder = "",
                                 required = false,
                             }: ReturnType<typeof useTextAreaState>) => {
    const textAreaRef = useRef<HTMLTextAreaElement>(null);

    useAutosize(textAreaRef, value);

    return (
        <div className={classes.field}>
            <label htmlFor={id} className={classes.label}>{required ? `*${label}` : label}</label>
            <textarea
                ref={textAreaRef}
                id={id}
                className={classes.textArea}
                maxLength={maxLength}
                value={value}
                onChange={onChange}
                placeholder={placeholder}
            />
        </div>
    );
}
