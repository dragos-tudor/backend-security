import { throwError } from "../support/errors/throwing.js"
import { isValidLanguage } from "./verifying.js"

export const validateLang = (lang) =>
  isValidLanguage(lang) || throwError(`Invalid language ${lang}`)