import {Route, Routes} from "react-router-dom";
import {HomePage} from "./view/pages/HomePage";
import {DictionaryPage} from "./view/pages/DictionaryPage/DictionaryPage.tsx";
import {NewWordPage} from "./view/pages/NewWordPage.tsx";
import {EditWordPage} from "./view/pages/EditWordPage.tsx";
import {CheckPage} from "./view/pages/CheckPage/CheckPage.tsx";
import {ResultPage} from "./view/pages/ResultPage.tsx";

export const AppView = () => (
    <Routes>
        <Route>
            <Route path="/" element={<HomePage/>}></Route>
            <Route path="/dictionary" element={<DictionaryPage/>}></Route>
            <Route path="/new-word" element={<NewWordPage/>}></Route>
            <Route path="/edit-word/:id" element={<EditWordPage/>}></Route>
            <Route path="/check" element={<CheckPage/>}></Route>
            <Route path="/result" element={<ResultPage/>}></Route>
        </Route>
    </Routes>
);
