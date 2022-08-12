import { writable } from "svelte/store";
import { pageConstants } from "./pages/PageConstants";

export const CurrentPageStore = writable(pageConstants.loginPage);
export const UserIdStore = writable(null);
export const AttemptStore = writable([]);