import type { ChangeEventHandler } from "react";
import type { FiledProps } from "./Field";

export const useFieldState = ({ setValue, ...props }: FiledProps) => {
  const onChange: ChangeEventHandler<HTMLInputElement> = ({
    target: { value }
  }) => {
    setValue(value);
  }

  return { ...props, onChange };
};
