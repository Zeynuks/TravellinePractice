import type {Feedback, FeedbackFeed} from "./domain";

const feedbackStorageKey = `feedbackStorage`;
const feedbackFeedStorageKey = `feedbackFeedStorage`;

const setFeedback = (value: Feedback) => localStorage.setItem(feedbackStorageKey, JSON.stringify(value));

const getFeedback = (): Feedback | undefined => {
    const item = localStorage.getItem(feedbackStorageKey) ?? undefined;

    return item === undefined ? undefined : JSON.parse(item);
};

const setFeedbackFeed = (value: FeedbackFeed) => localStorage.setItem(feedbackFeedStorageKey, JSON.stringify(value));

const getFeedbackFeed = (): FeedbackFeed | undefined => {
    const item = localStorage.getItem(feedbackFeedStorageKey) ?? undefined;

    return item === undefined ? undefined : JSON.parse(item);
};

export const AppStorage = {getFeedback, setFeedback, getFeedbackFeed, setFeedbackFeed};