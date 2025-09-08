import type {Currency} from "../../domain";
import classes from "./CurrencyItem.module.scss";

type CurrencyItemProps = {
    currency: Currency;
};


export const CurrencyItem = ({currency}: CurrencyItemProps) => {
    if (!currency) {
        return null;
    }

    return (
        <div className={classes.item}>
            <div className={classes.info}>
                <span className={classes.code}>{currency.code ?? "---"}</span>
                <span className={classes.name}>{currency.name ?? "Select code"}</span>
            </div>
            {currency.code && <img
                className={classes.flag}
                src={`/currency-icons/${currency.code.toLowerCase()}.svg`}
                alt={currency.code}
            />}

        </div>
    );
};