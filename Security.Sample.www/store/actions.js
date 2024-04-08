import { AppState } from "./states.js"
const { createAction } = await import("/scripts/states.js")

export const createSetUserAction = (user) =>
  createAction(`${AppState}/setUser`, {user})