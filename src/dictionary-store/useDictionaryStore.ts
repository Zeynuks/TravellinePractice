import {useContext} from "react";
import {DictionaryContext} from "./DictionaryContext.tsx";

export const useDictionaryStore = () => useContext(DictionaryContext);
