import {CleanlinessSlider} from "./Sliders/CleanlinessSlider.tsx";
import {ServiceSlider} from "./Sliders/ServiceSlider.tsx";
import {SpeedSlider} from "./Sliders/SpeedSlider.tsx";
import {LocationSlider} from "./Sliders/LocationSlider.tsx";
import {SpeechSlider} from "./Sliders/SpeechSlider.tsx";
import classes from "./FeedbackForm.module.scss"
import {UserNameField} from "./UserNameField.tsx";
import {ContentTextArea} from "./ContentTextArea.tsx";
import {FeedbackSubmitButton} from "./FeedbackSubmitButton.tsx";

/*
* Есть проблема лишнего ререндера, но это свойство кастомного store,
* если все объекты (использующие store) находятся в одном div, то всё работает корректно,
* но в случае, если они разнесены по разным, то происходят лишние ререндеры
* (объяснить для себя причину этого я не смог)
*/
export const FeedbackFormView = () => (
    <>
        <div className={classes.rating}>
            <CleanlinessSlider/>
            <ServiceSlider/>
            <SpeedSlider/>
            <LocationSlider/>
            <SpeechSlider/>
        </div>
        <div className={classes.about}>
            <UserNameField/>
            <ContentTextArea/>
            <FeedbackSubmitButton/>
        </div>
    </>
);