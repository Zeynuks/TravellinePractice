import {CurrencyExchanger} from "./view/CurrencyExchanger/CurrencyExchanger.tsx";
import {Loader} from "./view/components/Loader/Loader.tsx";
import type {AppProps} from "./App.tsx";

export const AppView = ({isConnected}: AppProps) => (
    <>
        {isConnected ? <CurrencyExchanger/> : <Loader message={"Connecting..."} />}
    </>
);
