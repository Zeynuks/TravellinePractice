import {useId} from "react";
import {useFeedbackScore} from "../../../hooks";
import {useFeedbackStore} from "../../../feedback-store";

export const useServiceSliderState = () => {
    const id = useId();
    const label = "Сервис";
    const feedbackScore = useFeedbackScore();
    const value = feedbackScore.service;
    const feedbackStore = useFeedbackStore();
    const setValue = (newValue: string) => feedbackStore.set({
        ...feedbackStore.getSnapshot(),
        score: {...feedbackScore, service: Number(newValue)}
    });

    return {id, label, value, setValue};
};
