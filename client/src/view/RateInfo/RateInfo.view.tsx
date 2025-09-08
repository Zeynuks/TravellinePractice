import classes from "./RateInfo.module.scss";
import arrowUp from "../../assets/icons/arrow-rate-up.svg"
import arrowDown from "../../assets/icons/arrow-rate-down.svg"
import type {useRateInfoState} from "./RateInfo.state.ts";

export const RateInfoView = ({
                                 currentRate,
                                 todayChange,
                                 todayChangePercent
                             }: ReturnType<typeof useRateInfoState>) => {
    const icon = todayChange > 0 ? arrowUp : todayChange < 0 ? arrowDown : undefined;

    return (
        <div className={classes.container}>
            <div className={classes.inner}>
                <span className={classes.label}>Current rate</span>
                <span className={classes.currentRate}>{currentRate}</span>
            </div>
            <div className={classes.inner}>
                <span className={classes.label}>Today’s change</span>
                {icon && <img className={classes.arrow} src={icon} alt={icon}/>}
                {todayChange > 0 &&
                    <span className={classes.TodayChangeUp}>{todayChange} ({todayChangePercent}%)</span>
                }
                {todayChange < 0 &&
                    <span className={classes.TodayChangeDown}>{todayChange} ({todayChangePercent}%)</span>
                }
                {todayChange == 0 &&
                    <span className={classes.TodayChangeStay}>{todayChange}</span>
                }
            </div>
        </div>
    )
}