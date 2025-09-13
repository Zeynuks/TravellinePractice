import {useNavigate, useParams} from "react-router-dom";
import {EditWordPageView} from "./EditWordPage.view.tsx";
import {useDictionary} from "../../hooks/useDictionary.ts";
import type {Word} from "../../domain";

export type EditWordPageProps = {
    id: number;
    word: Word;
}

export const EditWordPage = () => {
    const navigate = useNavigate();
    const {id} = useParams<{ id: string }>();

    if (!id) {
        navigate("/dictionary")
    }

    const wordId = Number(id);
    const dictionary = useDictionary()

    return <EditWordPageView id={wordId} word={dictionary[wordId]}/>;
};