import { getLocationParam } from "../locations/getting.js"
import { Languages } from "./languages.js";

export const LanguageParamName = "lang"

export const getDefaultLanguage = (languages) => languages.en

export const getLanguageParam = (location) =>
  getLocationParam(location, LanguageParamName) ?? getDefaultLanguage(Languages)