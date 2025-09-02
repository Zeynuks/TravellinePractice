import {Slider} from "../Slider/Slider.tsx";
import {useSpeedSliderState} from "./SpeedSlider.state.ts";

export const SpeedSlider = () => <Slider {...useSpeedSliderState()}/>;