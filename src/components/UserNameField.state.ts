import {useFeedbackUserName} from "../hooks";
import {useFeedbackStore} from "../feedback-store";
import {useId} from "react";

export const useFirstNameFieldState = () => {
    const id = useId();
    const label = "Имя";
    const value = useFeedbackUserName();
    const placeholder = "Как вас зовут?";
    const required = true;
    const feedbackStore = useFeedbackStore();
    const setValue = (newValue: string) => feedbackStore.set({...feedbackStore.getSnapshot(), userName: newValue});

    return { id, label, value, placeholder, required, setValue };
};
