import type { ChangeEventHandler } from "react";
import type {TextAreaProps} from "./TextArea.tsx";

export const useTextAreaState = ({ setValue, ...props }: TextAreaProps) => {
  const onChange: ChangeEventHandler<HTMLTextAreaElement> = ({
    target: { value },
  }) => setValue(value);

  return { ...props, onChange };
};
