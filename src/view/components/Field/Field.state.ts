import type { ChangeEventHandler } from "react";
import type {FieldProps} from "./Field.tsx";

export const useFieldState = ({ setValue, ...props }: FieldProps) => {
  const onChange: ChangeEventHandler<HTMLInputElement> = ({
    target: { value },
  }) => setValue(value);

  return { ...props, onChange };
};
