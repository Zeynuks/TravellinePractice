import classes from "./FeedbackFeed.module.scss";
import {Feedback} from "../components/Feedback/Feedback.tsx";
import type {FeedbackFeedProps} from "./FeedbackFeed.tsx";

export const FeedbackFeedView = ({feedbackFeed}: FeedbackFeedProps) => (
    <div className={classes.feed}>
        {feedbackFeed && feedbackFeed.map((feedback, i) => {
            const scores = Object.values(feedback.score);
            const averageScore = scores.length ?
                Math.round((scores.reduce((a, b) =>
                    a + b, 0) / scores.length) * 100) / 100 : 0;

            return (
                <Feedback
                    key={i}
                    userName={feedback.userName}
                    content={feedback.content}
                    score={averageScore}
                />
            );
        })}
    </div>
);