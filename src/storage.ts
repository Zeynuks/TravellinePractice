const LOCAL_STORAGE_KEY = "jsondiff.username";

export function getSavedUserName(): string | null {
    const savedName = localStorage.getItem(LOCAL_STORAGE_KEY);

    if (savedName && savedName.trim()) {
        return savedName.trim();
    }

    return null;
}

export function saveUserName(userName: string | null): void {
    if (userName === null) {
        localStorage.removeItem(LOCAL_STORAGE_KEY);
    } else {
        localStorage.setItem(LOCAL_STORAGE_KEY, userName);
    }
}
