export const parseNumber = (value: string): [string, boolean] => {
    const raw = value.trim();
    if (raw === "" || raw === "-" || raw === "+" || raw === "." || raw === "-." || raw === "+.") {
        return [value, false];
    }

    let s = raw;
    if (s.includes(",") && !s.includes(".")) {
        if ((s.match(/,/g) || []).length > 1) {
            return [value, false];
        }
        s = s.replace(",", ".");
    }

    if (/^[+-]?\.\d+([eE][-+]?\d+)?$/.test(s)) s = s.replace(/^\+?\./, "0.").replace(/^-\./, "-0.");

    const numRe = /^[+-]?(?:\d+(?:\.\d+)?|\d*\.?\d+)(?:[eE][-+]?\d+)?$/;
    if (!numRe.test(s)) {
        return [value, false];
    }

    const n = Number(s);
    if (!Number.isFinite(n)) {
        return [value, false];
    }

    const typedExp = /[eE]/.test(s);
    const abs = Math.abs(n);
    const needsExp = abs >= 1e6 || (abs > 0 && abs < 1e-3);

    if (needsExp) {
        return [n.toExponential(0).replace("+", ""), true];
    }

    if (typedExp) {
        return [String(n), true];
    }

    if (s.includes(".")) {
        return [n.toFixed(2).replace(/\.?0+$/, ""), true];
    }

    return [String(n), true];
};
