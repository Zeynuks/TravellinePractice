/* eslint-disable @typescript-eslint/no-explicit-any */
import {useSyncExternalStore} from "react";
import type {Store} from "./Store";

const identity = <TValue>(value: TValue) => value;

export const useStore = <TValue, TResult = TValue>(
    store: Store<TValue>,
    select: (state: TValue) => TResult = identity<TValue> as any,
) => useSyncExternalStore(store.subscribe, () => select(store.getSnapshot()));
