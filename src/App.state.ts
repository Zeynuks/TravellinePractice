import {AppStorage} from "./AppStorage";
import {useSubscribeStore} from "./store";
import {useDictionaryStore} from "./dictionary-store";

export const useAppState = () => {
    useSubscribeStore(useDictionaryStore(), (value) => AppStorage.setDictionary(value));
};