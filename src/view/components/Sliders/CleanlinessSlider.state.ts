import {useId} from "react";
import {useFeedbackScore} from "../../../hooks/useFeedbackScore.ts";
import {useFeedbackStore} from "../../../feedback-store";

export const useCleanlinessSliderState = () => {
    const id = useId();
    const label = "Чистенько";
    const feedbackScore = useFeedbackScore();
    const value = feedbackScore.cleanliness;
    const feedbackStore = useFeedbackStore();
    const setValue = (newValue: string) =>
        feedbackStore.set({
            ...feedbackStore.getSnapshot(),
        score: {...feedbackScore, cleanliness: Number(newValue)}
    });

    return {id, label, value, setValue};
};
