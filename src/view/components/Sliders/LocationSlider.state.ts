import {useId} from "react";
import {useFeedbackScore} from "../../../hooks/useFeedbackScore.ts";
import {useFeedbackStore} from "../../../feedback-store";

export const useLocationSliderState = () => {
    const id = useId();
    const label = "Место";
    const feedbackScore = useFeedbackScore();
    const value = feedbackScore.location;
    const feedbackStore = useFeedbackStore();
    const setValue = (newValue: string) => feedbackStore.set({
        ...feedbackStore.getSnapshot(),
        score: {...feedbackScore, location: Number(newValue)}
    });

    return {id, label, value, setValue};
};
