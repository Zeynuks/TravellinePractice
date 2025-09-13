import type {Word} from "./domain";

const localStorageKey = `dictionaryLocalStorage`;

const setDictionary = (value: Word[]) => localStorage.setItem(localStorageKey, JSON.stringify(value));

const getDictionary = (): Word[] | undefined => {
    const item = localStorage.getItem(localStorageKey) ?? undefined;

    return item === undefined ? undefined : JSON.parse(item);
};

export const AppStorage = {getDictionary, setDictionary};