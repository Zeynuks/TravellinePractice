import {useFeedbackFeedStore} from "../../feedback-feed-store";

export const useFeedbackFeed = () => {
    const feedbackFeedStore = useFeedbackFeedStore();
    const feedbackFeed = feedbackFeedStore.getSnapshot().feed;

    return {feedbackFeed};
};
