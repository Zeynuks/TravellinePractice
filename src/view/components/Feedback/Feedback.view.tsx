import classes from "./Feedback.module.scss";
import type {FeedbackProps} from "./Feedback.tsx";

export const FeedbackView = ({
                                 userName,
                                 content,
                                 score
                             }: FeedbackProps) => (
    <div className={classes.feedback}>
        <img className={classes.avatar} src={"/user-avatar.svg"} alt={"user-avatar"}/>
        <div className={classes.info}>
            <span className={classes.userName}>{userName}</span>
            <p className={classes.content}>{content}</p>
        </div>

        <span className={classes.score}>{score}/5</span>

    </div>
);
