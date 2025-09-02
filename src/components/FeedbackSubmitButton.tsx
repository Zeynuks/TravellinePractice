import {Button} from "./Button/Button.tsx";
import {useFeedbackSubmitButtonState} from "./FeedbackSubmitButton.state.ts";

export const FeedbackSubmitButton = () => <Button {...useFeedbackSubmitButtonState()}/>;