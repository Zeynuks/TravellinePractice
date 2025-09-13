import {useCheckPage} from "./CheckPage.state.ts";
import {CheckPageView} from "./CheckPage.view.tsx";

export const CheckPage = () => <CheckPageView {...useCheckPage()} />;