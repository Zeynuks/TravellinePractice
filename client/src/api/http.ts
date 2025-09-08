const API_BASE_URL = import.meta.env.API_URL ?? "http://localhost:5081";

export async function fetchJson<T>(path: string): Promise<T> {
    const response = await fetch(`${API_BASE_URL}${path}`, {
        headers: { Accept: "application/json" },
    });

    if (!response.ok) {
        const errorText = await response.text().catch(() => "");
        throw new Error(errorText || `HTTP ${response.status} ${response.statusText}`);
    }

    return response.json();
}

export function toQueryString(params: Record<string, string | undefined>) {
    const searchParams = new URLSearchParams();
    for (const [key, value] of Object.entries(params)) {
        if (value) searchParams.set(key, value);
    }
    const query = searchParams.toString();
    return query ? `?${query}` : "";
}

export async function checkConnection(path: string = "/currency"): Promise<boolean> {
    try {
        const response = await fetch(`${API_BASE_URL}${path}`, {
            method: "GET",
            headers: { Accept: "application/json" },
        });
        return response.ok;
    } catch {
        return false;
    }
}
