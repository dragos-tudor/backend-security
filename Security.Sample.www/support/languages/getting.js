import { getLocationParam } from "../locations/getting.js"

export const LanguageParamName = "lang"

export const getLanguageParam = (location) =>
  getLocationParam(location, LanguageParamName)