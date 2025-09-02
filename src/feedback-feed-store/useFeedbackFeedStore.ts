import {useContext} from "react";
import {FeedbackFeedContext} from "./FeedbackFeedContext.tsx";

export const useFeedbackFeedStore = () => useContext(FeedbackFeedContext);
