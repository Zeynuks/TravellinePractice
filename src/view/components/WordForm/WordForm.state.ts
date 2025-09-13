import {useDictionaryStore} from "../../../dictionary-store";
import type {WordFormProps} from "./WordForm.tsx";

export const useWordForm = ({id, word, ...props}: WordFormProps) => {
    const dictionaryStore = useDictionaryStore();

    const onSubmit = (value: string, translation: string) => {
        if (id !== undefined) {

            dictionaryStore.set(
                dictionaryStore.getSnapshot().map((item, i) =>
                    i === Number(id)
                        ? {word: value.trim(), translation: translation.trim()}
                        : item
                )
            );

        } else {
            dictionaryStore.set([
                ...dictionaryStore.getSnapshot(),
                {
                    word: value.trim(),
                    translation: translation.trim(),
                }
            ]);
        }
    }

    return {onSubmit, word, ...props}
}
