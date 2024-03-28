import { Languages } from "../support/languages/languages.js"
import { setLanguageParam } from "../support/languages/setting.js"

export const setEnglishParam = (searchParams) => setLanguageParam(searchParams, Languages.en)

export const setRomanianParam = (searchParams) => setLanguageParam(searchParams, Languages.ro)