import {Slider} from "../Slider/Slider.tsx";
import {useSpeechSliderState} from "./SpeechSlider.state.ts";

export const SpeechSlider = () => <Slider {...useSpeechSliderState()}/>;