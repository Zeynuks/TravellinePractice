import type {Currency} from "../../domain";
import classes from "./CurrencyDropdownItem.module.scss";

export const CurrencyMenuItem = (currency: Currency) => (
    <div className={classes.item}>
        <div className={classes.info}>
            <img className={classes.flag} src={`/currency-icons/${currency.code.toLowerCase()}.svg`} alt={currency.code}/>
            <span className={classes.name}>{currency.name}</span>
        </div>
        <span className={classes.code}>{currency.code}</span>
    </div>
);