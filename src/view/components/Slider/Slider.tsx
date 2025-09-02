import {useSliderState} from "./Slider.state.ts";
import {SliderView} from "./Slider.view.tsx";

export type SliderProps = {
    id: string;
    value: number;
    setValue: (value: string) => void;
    label?: string;
};

export const Slider = (props: SliderProps) => (
    <SliderView {...useSliderState(props)} />
);
