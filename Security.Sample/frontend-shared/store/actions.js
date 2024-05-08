import { AccountState, UserState } from "./states.js"
const { createAction } = await import("/scripts/states.js")

export const createIsAuthenticatedAction = (isAuthenticated) =>
  createAction(`${AccountState}/setAccount`, {isAuthenticated})

export const createSetUserAction = (user) =>
  createAction(`${UserState}/setUser`, {user})