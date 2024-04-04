import { throwError } from "../errors/throwing.js"
import { isValidLanguage } from "./verifying.js"

export const validateLanguage = (lang) =>
  isValidLanguage(lang) || throwError(`Invalid language ${lang}`)