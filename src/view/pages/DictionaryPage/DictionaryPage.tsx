import {DictionaryPageView} from "./DictionaryPage.view.tsx";
import {useDictionaryPage} from "./DictionaryPage.state.ts";

export const DictionaryPage = () => <DictionaryPageView {...useDictionaryPage()} />