import {Slider} from "../Slider/Slider.tsx";
import {useCleanlinessSliderState} from "./CleanlinessSlider.state.ts";

export const CleanlinessSlider = () => <Slider {...useCleanlinessSliderState()}/>;