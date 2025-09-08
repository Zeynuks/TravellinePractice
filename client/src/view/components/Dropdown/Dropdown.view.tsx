import {useEffect, useState} from "react";
import classes from "./Dropdown.module.scss";
import arrow from "../../../assets/icons/arrow-down.svg"
import search from "../../../assets/icons/search.svg"
import type {useDropdownState} from "./Dropdown.state.ts";

export const DropdownView = <T, >({
                                      label,
                                      current,
                                      itemComponent,
                                      items,
                                      onChange,
                                      filterFunction,
                                      limit,
                                      moreLabel
                                  }: ReturnType<typeof useDropdownState<T>>) => {
    const [isOpen, setOpen] = useState(false);
    const [expanded, setExpanded] = useState(false);
    const [currItems, setCurrItems] = useState<T[]>(items);
    const [value, setValue] = useState("");

    useEffect(() => {
        const query = value.trim().toLowerCase();
        setCurrItems(query ? filterFunction(items, query) : items);
    }, [filterFunction, items, value]);

    const visibleItems = limit !== undefined && !expanded ? currItems.slice(0, limit) : currItems;

    const hasMore = limit !== undefined && currItems.length > limit && moreLabel !== undefined;

    return (
        <div
            className={classes.dropdown}
            onMouseEnter={() => setOpen(true)}
            onMouseLeave={() => {
                setExpanded(false);
                setOpen(false);
            }}
        >
            <span className={classes.label}>{label}</span>
            <div className={classes.item}>
                {current}
                <img className={classes.arrow} src={arrow} alt={"arrow"}/>
            </div>
            <ul className={`${classes.menu} ${isOpen ? classes.open : ""}`}>
                {filterFunction && <div className={classes.item}>
                    <img className={classes.icon} src={search} alt={"search"}/>
                    <input className={classes.search}
                           value={value}
                           onChange={e => setValue(e.target.value)}
                           placeholder="Search"
                    />
                </div>}
                {visibleItems.map((item, i) => (
                    <li
                        key={i}
                        value={i}
                        className={classes.item}
                        onClick={() => {
                            onChange(item);
                            setValue("");
                            setOpen(false);
                        }}
                    >
                        {itemComponent(item)}
                    </li>
                ))}

                {hasMore && !expanded && (
                    <li
                        className={`${classes.item} ${classes.more}`}
                        onClick={() => setExpanded(true)}
                    >
                        {moreLabel}
                    </li>
                )}
            </ul>
        </div>
    );
}
