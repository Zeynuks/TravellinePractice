import {useEffect} from "react";

type TextareaRef = { current: HTMLTextAreaElement | null };

//Этот хук использует только textArea нужно ли его хранить вместе с остальными хуками или перенести к textArea
export const useAutosize = (ref: TextareaRef, value: string): void => {
    useEffect(() => {
        if (!ref) return;

        if (ref.current) {
            ref.current.style.height = "auto";

            const cs = window.getComputedStyle(ref.current);
            const border = parseFloat(cs.borderTopWidth) + parseFloat(cs.borderBottomWidth);

            ref.current.style.height = `${ref.current.scrollHeight + border}px`;
        }
    }, [ref, value]);
};
