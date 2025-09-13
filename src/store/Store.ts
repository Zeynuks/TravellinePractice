const create = <TValue>(initialValue: TValue) => {
    let listeners: (() => void)[] = [];
    let value: TValue = initialValue;

    const emitChange = () => {
        for (const listener of listeners) {
            listener();
        }
    };

    const set = (newValue: TValue) => {
        if (value === newValue) {
            return;
        }

        value = newValue;
        emitChange();
    };

    const subscribe = (listener: () => void) => {
        listeners = [...listeners, listener];

        return () => {
            listeners = listeners.filter((item) => item !== listener);
        };
    };

    const getSnapshot = () => value;

    return {getSnapshot, set, subscribe};
};

export type Store<TValue> = ReturnType<typeof create<TValue>>;

export const Store = {create};
