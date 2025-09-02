import {FeedbackFormView} from "./FeedbackForm.view.tsx";
import {useFeedbackForm} from "./FeedbackForm.state.ts";
import {Form} from "./Form/Form.tsx";

export const FeedbackForm = () => (
    <Form {...useFeedbackForm()}>
        <FeedbackFormView/>
    </Form>
);