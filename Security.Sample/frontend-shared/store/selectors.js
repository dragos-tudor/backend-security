import { AccountState, UserState } from "./states.js"

export const selectIsAuthenticated = states => states[AccountState].isAuthenticated

export const selectUser = states => states[UserState]
