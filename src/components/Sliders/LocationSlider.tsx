import {Slider} from "../Slider/Slider.tsx";
import {useLocationSliderState} from "./LocationSlider.state.ts";

export const LocationSlider = () => <Slider {...useLocationSliderState()}/>;