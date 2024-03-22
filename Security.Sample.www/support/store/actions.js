import { AppState } from "./creating.js"
const { createAction } = await import("/scripts/states.js")

export const createSetUserAction = (user) =>
  createAction(`${AppState}/setUser`, {user})

export const createSetLangAction = (lang) =>
  createAction(`${AppState}/setLang`, {lang})