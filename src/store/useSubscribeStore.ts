import {useEffect, useRef} from 'react';
import type {Store} from './Store';

export const useSubscribeStore = <TValue>(
    store: Store<TValue>,
    listener: (value: TValue) => void
) => {
    const listenerRef = useRef(listener);

    useEffect(() => {
        listenerRef.current = listener;
    }, [listener]);

    useEffect(() => {
        return store.subscribe(() => listenerRef.current(store.getSnapshot()));
    }, [store]);
};
