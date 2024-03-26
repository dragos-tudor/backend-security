import { getSearchParam } from "../locations/getting.js"
import { Languages } from "./languages.js";

export const getLanguageParam = (location) => getSearchParam(location, "lang")

export const getLanguage = (location) => getLanguageParam(location) ?? Languages.en