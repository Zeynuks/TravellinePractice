import {useId} from "react";
import {useFeedbackScore} from "../../hooks/useFeedbackScore.ts";
import {useFeedbackStore} from "../../feedback-store";

export const useSpeechSliderState = () => {
    const id = useId();
    const label = "Культура речи";
    const feedbackScore = useFeedbackScore();
    const value = feedbackScore.speech;
    const feedbackStore = useFeedbackStore();
    const setValue = (newValue: string) => feedbackStore.set({
        ...feedbackStore.getSnapshot(),
        score: {...feedbackScore, speech: Number(newValue)}
    });

    return {id, label, value, setValue};
};
