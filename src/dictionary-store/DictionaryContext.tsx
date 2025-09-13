import {createContext} from "react";
import {Store} from "../store";
import {AppStorage} from "../AppStorage";
import type {Word} from "../domain";

export const DictionaryContext = createContext<Store<Word[]>>(Store.create(AppStorage.getDictionary() ?? [{
    word: "1",
    translation: "en"
}]));
