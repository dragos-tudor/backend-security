const { createStoreState } = await import("/scripts/states.js")

export const AppState = "appState"

export const createAppState = (user, lang = "en") =>
  createStoreState(AppState, ({ user, lang }))