import {useId} from "react";
import {useFeedbackScore} from "../../../hooks/useFeedbackScore.ts";
import {useFeedbackStore} from "../../../feedback-store";

export const useSpeedSliderState = () => {
    const id = useId();
    const label = "Скорость";
    const feedbackScore = useFeedbackScore();
    const value = feedbackScore.speed;
    const feedbackStore = useFeedbackStore();
    const setValue = (newValue: string) =>
        feedbackStore.set({
            ...feedbackStore.getSnapshot(),
            score: {...feedbackScore, speed: Number(newValue)}
        });

    return {id, label, value, setValue};
};
