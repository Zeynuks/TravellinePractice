import {FeedbackFeedView} from "./FeedbackFeed.view.tsx";
import type {Feedback} from "../../domain";
import {useFeedbackFeed} from "./FeedbackFeed.state.ts";

export type FeedbackFeedProps = {
    feedbackFeed: Array<Feedback>;
}

export const FeedbackFeed = () => <FeedbackFeedView {...useFeedbackFeed()}/>