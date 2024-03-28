import { LanguageParamName } from "./getting.js"

export const setLanguageParam = (searchParams, lang) =>
  searchParams.set(LanguageParamName, lang)
