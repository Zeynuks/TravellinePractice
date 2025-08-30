export type JsonPrimitive = string | number | boolean | null;
export type JsonValue = JsonPrimitive | JsonObject | JsonArray;
export type JsonArray = JsonValue[];
export type JsonObject = { [k: string]: JsonValue };

export type DiffEntry =
    | { type: "unchanged"; oldValue: JsonValue; newValue: JsonValue }
    | { type: "changed"; oldValue: JsonValue; newValue: JsonValue }
    | { type: "deleted"; oldValue: JsonValue }
    | { type: "new"; newValue: JsonValue };

export type DiffResult = { [k: string]: DiffEntry };

export function isJsonValue(v: unknown): v is JsonValue {
    if (v === null) {
        return true;
    }
    if (typeof v === "string" || typeof v === "number" || typeof v === "boolean") {
        return true;
    }
    if (Array.isArray(v)) {
        for (const element of v) {
            if (!isJsonValue(element)) {
                return false;
            }
        }

        return true;
    }
    if (typeof v === "object") {
        const obj = v as Record<string, unknown>;

        const keys = Object.keys(obj);

        for (const k of keys) {
            if (!isJsonValue(obj[k])) {
                return false;
            }
        }

        return true;
    }

    return false;
}

export function isJsonObject(v: unknown): v is JsonObject {
    if (v && typeof v === "object" && !Array.isArray(v)) {
        return isJsonValue(v);
    }

    return false;
}

export function parseJsonObject(text: string): { ok: true; value: JsonObject } | { ok: false } {
    try {
        const parsed = JSON.parse(text);

        if (isJsonObject(parsed)) {
            return {ok: true, value: parsed};
        }

        return {ok: false};
    } catch {
        return {ok: false};
    }
}

export function deepEqual(a: JsonValue, b: JsonValue): boolean {
    if (a === b) {
        return true;
    }
    if (typeof a !== typeof b) {
        return false;
    }
    if (Array.isArray(a) && Array.isArray(b)) {
        if (a.length !== b.length) {
            return false;
        }
        for (const [i, element] of a.entries()) {
            if (!deepEqual(element, b[i])) {
                return false;
            }
        }

        return true;
    }
    if (Array.isArray(a) || Array.isArray(b)) {
        return false;
    }
    if (a && b && typeof a === "object" && typeof b === "object") {
        const aObj = a;

        const bObj = b;

        const aKeys = Object.keys(aObj);

        const bKeys = Object.keys(bObj);

        if (aKeys.length !== bKeys.length) {
            return false;
        }
        for (const k of aKeys) {
            if (!Object.prototype.hasOwnProperty.call(bObj, k)) {
                return false;
            }
            if (!deepEqual(aObj[k], bObj[k])) {
                return false;
            }
        }

        return true;
    }

    return false;
}

export function buildDiff(oldObj: JsonObject, newObj: JsonObject): DiffResult {
    const result: DiffResult = {};

    const keys = new Set<string>([...Object.keys(oldObj), ...Object.keys(newObj)]);

    keys.forEach((key) => {
        const hasOld = Object.prototype.hasOwnProperty.call(oldObj, key);

        const hasNew = Object.prototype.hasOwnProperty.call(newObj, key);

        if (hasOld && hasNew) {
            const oldValue = oldObj[key];

            const newValue = newObj[key];

            if (deepEqual(oldValue, newValue)) {
                result[key] = {type: "unchanged", oldValue: oldValue, newValue: newValue};
            } else {
                result[key] = {type: "changed", oldValue: oldValue, newValue: newValue};
            }
        } else if (hasOld && !hasNew) {
            result[key] = {type: "deleted", oldValue: oldObj[key]};
        } else if (!hasOld && hasNew) {
            result[key] = {type: "new", newValue: newObj[key]};
        }
    });

    return result;
}
