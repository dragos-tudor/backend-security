import { AppState } from "./states.js"
const { createReducer } = await import("/scripts/states.js")

export const createAppReducer = () => createReducer(AppState, {
  setUser: (state, action) => ({ ...state, ...action.payload })
})