import { useFieldState } from "./Field.state";
import { FieldView } from "./Field.view";

export type FiledProps = {
  label?: string;
  value: string;
  setValue: (value: string) => void;
};

export const Field = (props: FiledProps) => (
  <FieldView {...useFieldState(props)}></FieldView>
);
