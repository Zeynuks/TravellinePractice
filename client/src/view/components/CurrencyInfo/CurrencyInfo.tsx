import {CurrencyInfoView} from "./CurrencyInfo.view.tsx";
import type {Currency} from "../../../domain";

export type CurrencyInfoProps = {
  currency: Currency;
};

export const CurrencyInfo = (props: CurrencyInfoProps) => (
  <CurrencyInfoView {...props}></CurrencyInfoView>
);
