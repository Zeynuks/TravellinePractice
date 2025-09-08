import {useEffect, useState} from "react";
import {AppView} from "./App.view";
import {useAppState} from "./App.state";
import {checkConnection} from "./api/http.ts";

export type AppProps = {
    isConnected?: boolean;
}

const App = () => {
    useAppState();

    const [isConnected, setIsConnected] = useState<boolean>(false);

    useEffect(() => {
        const check = () => {
            checkConnection().then(setIsConnected)
                .catch(() => setIsConnected(false));
        };

        check();

        const interval = setInterval(check, 1000);

        return () => clearInterval(interval);
    }, []);

    return <AppView isConnected={isConnected}/>;
};

export default App;
