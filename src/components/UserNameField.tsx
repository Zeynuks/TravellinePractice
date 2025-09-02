import {Field} from "./Field/Field.tsx";
import {useFirstNameFieldState} from "./UserNameField.state.ts";

export const UserNameField = () => <Field {...useFirstNameFieldState()}/>;