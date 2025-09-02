import {useFeedbackStore} from "../feedback-store";
import {useStore} from "../store";

export const useFeedbackContent = () => useStore(useFeedbackStore(), (state) => state.content);
