import {usePaymentCurrency} from "../../hooks";
import {CurrencyInfo} from "../components/CurrencyInfo/CurrencyInfo.tsx";

export const CurrencyFromInfo = () => {
    const  currency = usePaymentCurrency();

    return <CurrencyInfo currency={currency}/>
}