import { Languages } from "./languages.js"

export const isEnglishLanguage = (lang) => Languages.en === lang

export const isRomanianLanguage = (lang) => Languages.ro === lang

export const isValidLanguage = (lang) => lang in Languages