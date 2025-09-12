import type {CurrencyDropdownProps} from "./CurrencyDropdown.tsx";
import type {Currency} from "../../domain";
import {CurrencyMenuItem} from "./CurrencyMenuItem.tsx";
import {useCurrencies} from "../../hooks/useCurrencies.ts";

export const useCurrencyDropdown = (props: CurrencyDropdownProps) => {
    const itemComponent = CurrencyMenuItem;
    const items = useCurrencies();

    const filterFunction = (list: Currency[], query: string): Currency[] => {
        const q = query.trim().toLowerCase();
        if (!q) return [];
        return list.filter(c =>
            c.code.toLowerCase().includes(q.toLowerCase()) ||
            c.name.toLowerCase().includes(q.toLowerCase())
        );
    }

    const limit = 4;
    const moreLabel = "Other currencies";

    return {...props, itemComponent, items, filterFunction, limit, moreLabel};
};