import { AppState } from "./creating.js"

export const selectUser = states => states[AppState].user

export const selectUserAuthenticated = states => !!states[AppState].user