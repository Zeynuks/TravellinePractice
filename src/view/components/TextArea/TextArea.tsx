import {TextAreaView} from "./TextArea.view.tsx";
import {useTextAreaState} from "./TextArea.state.ts";

export type TextAreaProps = {
    id: string;
    value: string;
    setValue: (value: string) => void;
    label?: string;
    maxLength?: number;
    placeholder?: string;
    required?: boolean;
};

export const TextArea = (props: TextAreaProps) => (
    <TextAreaView {...useTextAreaState(props)}></TextAreaView>
);
