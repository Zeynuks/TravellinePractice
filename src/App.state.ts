import {AppStorage} from "./AppStorage";
import {useFeedbackStore} from "./feedback-store";
import {useSubscribeStore} from "./store";
import {useFeedbackFeedStore} from "./feedback-feed-store";

export const useAppState = () => {
    useSubscribeStore(useFeedbackStore(), (value) => AppStorage.setFeedback(value));
    useSubscribeStore(useFeedbackFeedStore(), (value) => AppStorage.setFeedbackFeed(value));
};