import {Slider} from "../Slider/Slider.tsx";
import {useServiceSliderState} from "./ServiceSlider.state.ts";

export const ServiceSlider = () => <Slider {...useServiceSliderState()}/>;