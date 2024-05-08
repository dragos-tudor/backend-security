import { AccountState, UserState } from "./states.js"
const { createReducer } = await import("/scripts/states.js")

export const createAccountReducer = () => createReducer(AccountState, {
  setAccount: (state, action) => ({ ...state, ...action.payload })
})

export const createUserReducer = () => createReducer(UserState, {
  setUser: (state, action) => ({ ...state, ...action.payload })
})