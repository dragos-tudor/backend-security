import { AccountState, UserState } from "./states.js"
const { createAction } = await import("/scripts/states.js")

export const createAuthenticatedAction = (authenticated) =>
  createAction(`${AccountState}/setAccount`, {authenticated})

export const createSetUserAction = (user) =>
  createAction(`${UserState}/setUser`, {user})