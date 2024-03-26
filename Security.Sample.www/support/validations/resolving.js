import { isEnglishLanguage } from "../languages/verifying.js";
import { importValidationErrors } from "./importing.js";

const { getValidationErrors } = await import("/scripts/validating.js")

export const resolveValidationErrors = async (lang) =>
  isEnglishLanguage(lang)? getValidationErrors(): await importValidationErrors(lang)