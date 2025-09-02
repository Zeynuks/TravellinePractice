import {Button} from "../components/Button/Button.tsx";
import {useFeedbackSubmitButtonState} from "./FeedbackSubmitButton.state.ts";

export const FeedbackSubmitButton = () => <Button {...useFeedbackSubmitButtonState()}/>;