import { AccountState, UserState } from "./states.js"

export const selectAuthenticated = states => states[AccountState].authenticated

export const selectUser = states => states[UserState]
