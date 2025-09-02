import {FeedbackFormView} from "./FeedbackForm.view.tsx";
import {useFeedbackForm} from "./FeedbackForm.state.ts";
import {Form} from "../components/Form/Form.tsx";

export const FeedbackForm = () => (
    <Form {...useFeedbackForm()}>
        <FeedbackFormView/>
    </Form>
);