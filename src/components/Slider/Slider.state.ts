import type { ChangeEventHandler } from "react";
import type {SliderProps} from "./Slider.tsx";

export const useSliderState = ({ setValue, ...props }: SliderProps) => {
  const onChange: ChangeEventHandler<HTMLInputElement> = ({
    target: { value },
  }) => setValue(value);

  return { ...props, onChange };
};
