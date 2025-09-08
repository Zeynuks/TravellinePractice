import {DropdownView} from "./Dropdown.view.tsx";
import {useDropdownState} from "./Dropdown.state.ts";

export type DropdownProps<T> = {
    label?: string,
    current: React.ReactNode;
    itemComponent: (item: T) => React.ReactNode;
    items: T[];
    setValue: (value: T) => void;
    filterFunction?: (items: T[], query: string) => T[];
    limit?: number;
    moreLabel?: string;
};

export const Dropdown = <T, >(props: DropdownProps<T>) => (
    <DropdownView<T> {...useDropdownState(props)}/>
);