const { createReducer, createStoreState } = await import("/scripts/states.js")

export const AppState = "appState"

export const createAppState = (user, lang = "en") =>
  createStoreState(AppState, ({ user, lang }))

export const createAppReducer = () => createReducer(AppState, {
  setUser: (state, action) => ({ ...state, ...action.payload }),
  setLang: (state, action) => ({ ...state, ...action.payload })
})