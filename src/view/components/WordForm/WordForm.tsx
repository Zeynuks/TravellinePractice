import {WordFormView} from "./WordForm.view.tsx";
import {useWordForm} from "./WordForm.state.ts";
import type {Word} from "../../../domain";

export type WordFormProps = {
    id?: number;
    word?: Word;
}

export const WordForm = (props: WordFormProps) => <WordFormView {...useWordForm(props)}/>