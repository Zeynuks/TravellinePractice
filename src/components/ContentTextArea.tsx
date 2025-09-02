import {TextArea} from "./TextArea/TextArea.tsx";
import {useContentTextAreaState} from "./ContentTextArea.state.ts";

export const ContentTextArea = () => <TextArea {...useContentTextAreaState()}/>;