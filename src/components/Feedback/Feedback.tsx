import {FeedbackView} from "./Feedback.view.tsx";

export type FeedbackProps = {
    userName: string;
    content: string;
    score: number;
};

export const Feedback = (props: FeedbackProps) => (
    <FeedbackView {...props} />
);
