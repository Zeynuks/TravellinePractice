import {useDictionary} from "../../../hooks/useDictionary.ts";
import {useDictionaryStore} from "../../../dictionary-store";

export const useDictionaryPage = () => {
    const dictionaryStore = useDictionaryStore();
    const dictionary = useDictionary();

    const deleteWord = (id: number) => {
        dictionaryStore.set([...dictionaryStore.getSnapshot().filter((_, i) => i !== id)]);
    }

    return {dictionary, deleteWord};
}