import {useFeedbackStore} from "../feedback-store";
import {useStore} from "../store";

export const useFeedbackUserName = () => useStore(useFeedbackStore(), (state) => state.userName);
