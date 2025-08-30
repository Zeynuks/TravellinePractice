import {buildDiff, parseJsonObject, type DiffResult} from "./diff.js";
import {getSavedUserName, saveUserName} from "./storage.js";

const heroSection = document.querySelector<HTMLElement>(".hero");

const loginSection = document.querySelector<HTMLElement>(".login");

const compareSection = document.querySelector<HTMLElement>(".compare");

const loginButton = document.querySelector<HTMLButtonElement>(".header__login");

const userBox = document.querySelector<HTMLDivElement>(".header__user");

const userNameSpan = document.querySelector<HTMLSpanElement>(".header__name");

const logoutButton = document.querySelector<HTMLButtonElement>(".header__logout");

const startButton = document.querySelector<HTMLButtonElement>(".hero__button");

const loginForm = document.querySelector<HTMLFormElement>(".login__form");

const loginInput = document.querySelector<HTMLInputElement>("#login-input");

const loginErrorMessage = document.querySelector<HTMLSpanElement>(".login__error-message");

const compareForm = document.querySelector<HTMLFormElement>(".compare__form");

const oldJsonTextarea = document.querySelector<HTMLTextAreaElement>("#old-json");

const newJsonTextarea = document.querySelector<HTMLTextAreaElement>("#new-json");

const compareResult = document.querySelector<HTMLElement>(".compare__result");

const oldJsonError = (() => {
    const parent = oldJsonTextarea ? oldJsonTextarea.parentElement : null;

    if (parent) {
        const span = parent.querySelector(".compare__error");

        if (span instanceof HTMLSpanElement) {
            return span;
        }
    }

    return null;
})();

const newJsonError = (() => {
    const parent = newJsonTextarea ? newJsonTextarea.parentElement : null;

    if (parent) {
        const span = parent.querySelector(".compare__error");

        if (span instanceof HTMLSpanElement) {
            return span;
        }
    }

    return null;
})();

function setElementVisible(element: Element | null, isVisible: boolean): void {
    if (element) {
        element.toggleAttribute("hidden", !isVisible);
    }
}

function showHeroPage(userName: string | null): void {
    setElementVisible(heroSection, true);
    setElementVisible(loginSection, false);
    setElementVisible(compareSection, false);
    setElementVisible(loginButton, !userName);
    setElementVisible(userBox, !!userName);
    setElementVisible(startButton, !!userName);
    if (userNameSpan) {
        userNameSpan.textContent = userName ?? "";
    }
}

function showLoginPage(): void {
    setElementVisible(heroSection, false);
    setElementVisible(compareSection, false);
    setElementVisible(loginSection, true);
    if (loginErrorMessage) {
        loginErrorMessage.hidden = true;
    }
    if (loginInput) {
        loginInput.value = "";
        loginInput.focus();
    }
}

function showComparePage(): void {
    setElementVisible(heroSection, false);
    setElementVisible(loginSection, false);
    setElementVisible(compareSection, true);
}

function handleLoginFormSubmit(event: SubmitEvent): void {
    event.preventDefault();
    const enteredName = loginInput && loginInput.value ? loginInput.value.trim() : "";

    if (!enteredName) {
        if (loginErrorMessage) {
            loginErrorMessage.hidden = false;
        }

        return;
    }
    saveUserName(enteredName);
    showHeroPage(enteredName);
}

function handleStartClick(): void {
    const savedName = getSavedUserName();

    if (savedName) {
        showComparePage();
    } else {
        showLoginPage();
    }
}

function handleLogout(): void {
    saveUserName(null);
    showHeroPage(null);
}

function handleCompareSubmit(event: SubmitEvent): void {
    event.preventDefault();

    const oldText = oldJsonTextarea ? oldJsonTextarea.value : "";

    const newText = newJsonTextarea ? newJsonTextarea.value : "";

    const oldParsed = parseJsonObject(oldText);

    const newParsed = parseJsonObject(newText);

    if (oldJsonError) {
        oldJsonError.hidden = true;
    }
    if (newJsonError) {
        newJsonError.hidden = true;
    }

    let hasError = false;

    if (!oldParsed.ok) {
        if (oldJsonError) {
            oldJsonError.hidden = false;
        }
        hasError = true;
    }
    if (!newParsed.ok) {
        if (newJsonError) {
            newJsonError.hidden = false;
        }
        hasError = true;
    }

    if (hasError) {
        if (compareResult) {
            compareResult.textContent = "";
        }

        return;
    }

    if (oldParsed.ok && newParsed.ok) {
        const diff: DiffResult = buildDiff(oldParsed.value, newParsed.value);

        if (compareResult) {
            compareResult.textContent = JSON.stringify(diff, null, 2);
        }
    }
}

function initPage(): void {
    const savedName = getSavedUserName();

    if (savedName) {
        showHeroPage(savedName);
    } else {
        showHeroPage(null);
    }
}

if (loginButton) {
    loginButton.addEventListener("click", showLoginPage);
}
if (logoutButton) {
    logoutButton.addEventListener("click", handleLogout);
}
if (startButton) {
    startButton.addEventListener("click", handleStartClick);
}
if (loginForm) {
    loginForm.addEventListener("submit", handleLoginFormSubmit);
}
if (loginInput) {
    loginInput.addEventListener("input", () => {
        if (loginErrorMessage) {
            loginErrorMessage.hidden = loginInput.value.trim().length > 0;
        }
    });
}
if (compareForm) {
    compareForm.addEventListener("submit", handleCompareSubmit);
}

initPage();
