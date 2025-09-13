import {useStore} from "../store";
import {useDictionaryStore} from "../dictionary-store";

export const useDictionary = () => useStore(useDictionaryStore(), (state) => state);
