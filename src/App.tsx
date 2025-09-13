import {AppView} from "./App.view";
import {useAppState} from "./App.state";

const App = () => {
    useAppState();

    return <AppView/>;
};

export default App;
