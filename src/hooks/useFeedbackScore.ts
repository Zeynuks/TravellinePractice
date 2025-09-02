import {useFeedbackStore} from "../feedback-store";
import {useStore} from "../store";

export const useFeedbackScore = () => useStore(useFeedbackStore(), (state) => state.score);
