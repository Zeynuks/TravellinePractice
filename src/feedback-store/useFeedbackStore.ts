import {useContext} from "react";
import {FeedbackContext} from "./FeedbackContext.tsx";

export const useFeedbackStore = () => useContext(FeedbackContext);
