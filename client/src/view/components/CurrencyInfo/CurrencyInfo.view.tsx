import type {CurrencyInfoProps} from "./CurrencyInfo.tsx";
import classes from "./CurrencyInfo.module.scss";

export const CurrencyInfoView = ({currency}: CurrencyInfoProps) => (
    <div className={classes.info}>
      <span className={classes.name}>{currency.name}</span>
      <span className={classes.description}>{currency.description}</span>
    </div>
);
