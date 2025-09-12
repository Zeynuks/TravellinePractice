import {usePurchasedCurrency} from "../../hooks";
import {CurrencyInfo} from "../components/CurrencyInfo/CurrencyInfo.tsx";

export const CurrencyToInfo = () => {
    const  currency = usePurchasedCurrency();

    return <CurrencyInfo currency={currency}/>
}