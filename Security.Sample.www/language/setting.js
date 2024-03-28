import { Languages } from "../support/languages/languages.js"
import { LanguageParamName } from "../support/languages/param.name.js"
import { setLocationParam } from "../support/locations/setting.js"

export const setEnglishParam = (searchParams) => setLocationParam(searchParams, LanguageParamName, Languages.en)

export const setRomanianParam = (searchParams) => setLocationParam(searchParams, LanguageParamName, Languages.ro)