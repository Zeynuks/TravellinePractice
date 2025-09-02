import {useFeedbackStore} from "../../feedback-store";
import {useSubscribeStore} from "../../store";
import {useEffect, useState} from "react";
import type {ButtonType} from "../components/Button/Button.tsx";
import type {Feedback} from "../../domain";

export const useFeedbackSubmitButtonState = () => {
    const label = "Отправить";
    const type: ButtonType = "submit";
    const [disabled, setDisabled] = useState<boolean>(true);
    const snapshot = useFeedbackStore().getSnapshot();

    const validateFeedback = (feedback: Feedback): boolean => {

        if (!feedback || !feedback.userName || !feedback.content) {
            return false;
        }

        return Object.values(feedback.score).every(val => val > 0);
    };


    useSubscribeStore(useFeedbackStore(), (value) => {

        if (validateFeedback(value)) {
            setDisabled(false);
            return;
        }

        setDisabled(true);
    })

    useEffect(() => {
        if (validateFeedback(snapshot)) {
            setDisabled(false);
            return;
        }
    }, []);

    return {label, type, disabled};
};
