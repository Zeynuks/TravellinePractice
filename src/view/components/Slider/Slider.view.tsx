import type {useSliderState} from "./Slider.state.ts";
import classes from "./Slider.module.scss";

export const SliderView = ({
                               id,
                               value,
                               onChange,
                               label = ""
                           }: ReturnType<typeof useSliderState>) => {

    const colors: Record<number, string> = {
        1: "#F24E1E",
        2: "#FF8311",
        3: "#FF8311",
        4: "#FFC700",
        5: "#FFC700"
    };

    const smiles: Record<number, string> = {
        1: "angry-face",
        2: "slightly-frowning-face",
        3: "neutral-face",
        4: "slightly-smiling-face",
        5: "grinning-face-with-big-eyes"
    }

    return (
        <div className={classes.field}>
            <div className={classes.rangeInput}>
                <input
                    id={id}
                    type="range"
                    min={1}
                    max={5}
                    step={1}
                    className={classes.input}
                    value={value}
                    onChange={onChange}
                    style={{accentColor: colors[value]}}
                    required
                />
                <div className={classes.bar}/>
                <div className={classes.bar}
                     style={{
                         width: `${((value - 1) / 4) * 100}%`,
                         background: colors[value],
                         boxShadow: "none",
                     }}
                />
                <div className={classes.tickets}>
                    {Array.from({length: 5}).map((_, i) => (
                        <span key={i}
                              className={classes.tick}
                              style={value - 1 > i ? {background: colors[value]}
                                  : value <= 0 || value >= 5 ? {background: colors[i + 1]} : {}
                              }
                        />
                    ))}
                </div>
                {/* Честно - перебирал значения чтобы работало( */}
                {(value >= 1 && value <= 5) &&
                    <img className={classes.smile}
                         src={`/src/assets/smiles/${smiles[value]}.svg`}
                         style={{left: `calc(-3px + -2.5px * ${value - 1} + (25% * ${value - 1})`}}
                         alt={smiles[value]}
                    />
                }
            </div>
            <label htmlFor={id} className={classes.label}>{label}</label>
        </div>
    );

}