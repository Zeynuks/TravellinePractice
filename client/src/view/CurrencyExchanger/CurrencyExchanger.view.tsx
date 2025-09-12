import classes from "./CurrencyExchanger.module.scss";
import {CurrencyFromDropdown} from "../CurrencySelector/CurrencyFromDropdown/CurrencyFromDropdown.tsx";
import {CurrencyToDropdown} from "../CurrencySelector/CurrencyToDropdown/CurrencyToDropdown.tsx";
import {CurrencyFromField} from "../CurrencyField/CurrencyFromField.tsx";
import {CurrencyToField} from "../CurrencyField/CurrencyToField.tsx";
import {RateInfo} from "../RateInfo/RateInfo.tsx";
import {RateChart} from "../components/RateChart/RateChart.tsx";
import {CurrencyFromInfo} from "../CurrencyInfo/CurrencyFromInfo.tsx";
import {CurrencyToInfo} from "../CurrencyInfo/CurrencyToInfo.tsx";

export const CurrencyExchangerView = () => {

    return (<div className={classes.container}>
        <h1 className={classes.title}>Always get the real exchange rate</h1>
        <span className={classes.description}>The most up-to-date exchange rates are always at your fingertips.</span>
        <div className={classes.exchanger}>
            <CurrencyFromDropdown/>
            <CurrencyToDropdown/>
            <CurrencyFromField/>
            <CurrencyToField/>
            <RateInfo/>
            <RateChart/>
            <CurrencyFromInfo/>
            <CurrencyToInfo/>
        </div>
    </div>)
}