import {useMemo, useState} from "react";
import {useNavigate} from "react-router-dom";
import {useDictionary} from "../../../hooks/useDictionary.ts";

const shuffle = <T, >(arr: T[]) => {
    const res = [...arr];
    for (let i = res.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [res[i], res[j]] = [res[j], res[i]];
    }
    return res;
};

export const useCheckPage = () => {
    const navigate = useNavigate();
    const dictionary = useDictionary();

    if (!dictionary || dictionary.length === 0) {
        navigate("/");
    }

    const words = useMemo(() => (dictionary ? shuffle(dictionary.map((w) => w.word)) : []), [dictionary]);
    const translations = useMemo(() => (dictionary ? dictionary.map((w) => w.translation) : []), [dictionary]);

    const [current, setCurrent] = useState(0);
    const [correct, setCorrect] = useState(0);
    const [incorrect, setIncorrect] = useState(0);

    const checkWord = (word: string, chosen: string) => {
        const expected = dictionary.find((w) => w.word === word)?.translation;
        const isCorrect = expected === chosen;

        setCorrect((c) => c + (isCorrect ? 1 : 0));
        setIncorrect((c) => c + (isCorrect ? 0 : 1));

        if (current + 1 >= words.length) {
            navigate("/result", {
                state: {
                    correct: correct + (isCorrect ? 1 : 0),
                    incorrect: incorrect + (isCorrect ? 0 : 1),
                    total: words.length,
                },
            });
        } else {
            setCurrent((s) => s + 1);
        }
    };

    return {current, words, translations, checkWord};
};
