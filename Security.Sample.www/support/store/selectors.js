import { AppState } from "./states.js"

export const selectUser = states => states[AppState].user

export const selectUserAuthenticated = states => !!states[AppState].user