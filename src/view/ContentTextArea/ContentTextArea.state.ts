import {useFeedbackStore} from "../../feedback-store";
import {useFeedbackContent} from "../../hooks/useFeedbackContent.ts";
import {useId} from "react";

export const useContentTextAreaState = () => {
    const id = useId();
    const placeholder = "Напишите, что понравилось, что было непонятно";
    const value = useFeedbackContent();
    const feedbackStore = useFeedbackStore();
    const setValue = (newValue: string) => feedbackStore.set({...feedbackStore.getSnapshot(), content: newValue});

    return { id, value, placeholder, setValue };
};
