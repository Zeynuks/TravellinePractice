import {useFeedbackStore} from "../feedback-store";
import {useFeedbackFeedStore} from "../feedback-feed-store";
import {defaultFeedback} from "../domain/DefaultFeedback.ts";

export const useFeedbackForm = () => {
    const label = "Помогите нам сделать процесс бронирования лучше";

    const feedbackFeedStore = useFeedbackFeedStore();
    const feedbackStore = useFeedbackStore();

    const onSubmit = () => {
        const prev = feedbackFeedStore.getSnapshot();
        const feedback = feedbackStore.getSnapshot();

        feedbackFeedStore.set({
            ...prev,
            feed: [feedback, ...prev.feed],
        });

        feedbackStore.set(defaultFeedback);
    };

    return {label, onSubmit};
};
