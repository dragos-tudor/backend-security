import { getValidationErrorsPath } from "./getting.js"

export const importValidationErrors = async (lang) => (await import(getValidationErrorsPath(lang))).default