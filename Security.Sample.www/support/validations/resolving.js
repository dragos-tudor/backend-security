import { isEnglishLanguage } from "../languages/verifying.js"
import { importValidationErrors } from "./importing.js";
const { ValidationErrors } = await import("/scripts/validating.js")

export const resolveValidationErrors = async (lang) =>
  isEnglishLanguage(lang)? ValidationErrors: await importValidationErrors(lang)