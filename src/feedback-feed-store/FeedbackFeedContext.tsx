import {createContext} from "react";
import {Store} from "../store";
import {AppStorage} from "../AppStorage";
import type {FeedbackFeed} from "../domain";

export const FeedbackFeedContext = createContext<Store<FeedbackFeed>>(Store.create(AppStorage.getFeedbackFeed() ?? {
    feed: [],
}));
