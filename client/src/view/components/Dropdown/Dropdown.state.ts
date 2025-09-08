import type {DropdownProps} from "./Dropdown.tsx";

export const useDropdownState = <T, >({setValue, ...props}: DropdownProps<T>) => {
    const onChange = (item) => {
        setValue(item);
    };

    return {...props, onChange};
};
