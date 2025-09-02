import {createContext} from "react";
import {Store} from "../store";
import {AppStorage} from "../AppStorage";
import type {Feedback} from "../domain";
import {defaultFeedback} from "../domain/DefaultFeedback.ts";

export const FeedbackContext = createContext<Store<Feedback>>(Store.create(AppStorage.getFeedback() ?? defaultFeedback));
