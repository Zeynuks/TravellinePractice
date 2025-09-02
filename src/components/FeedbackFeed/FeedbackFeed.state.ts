import {useFeedbackFeedStore} from "../../feedback-feed-store";
import type {FeedbackFeedProps} from "./FeedbackFeed.tsx";

export const useFeedbackFeed = (props: FeedbackFeedProps) => {
    const feedbackFeedStore = useFeedbackFeedStore();
    const feedbackFeed = feedbackFeedStore.getSnapshot().feed;

    return {...props, feedbackFeed};
};
