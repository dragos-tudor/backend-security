import { throwError } from "../support/errors/throwing.js"
import { getLabelsUrl, getValidationErrorsUrl } from "./getting.js"
import { setLabels } from "./labels.js"
import { isEnglishLanguage, isValidLanguage } from "./verifying.js"
const { getValidationErrors, setValidationErrors } = await import("/scripts/validating.js")

export const setLanguage = async (lang) =>
  isValidLanguage(lang)?
    {
      labels: setLabels((await import(getLabelsUrl(lang))).default),
      validationErrors: isEnglishLanguage(lang)?
        getValidationErrors():
        setValidationErrors((await import(getValidationErrorsUrl(lang))).default)
    }:
    throwError(`Invalid language ${lang}`)
