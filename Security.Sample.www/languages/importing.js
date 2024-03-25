import { getLabelsUrl, getValidationErrorsUrl } from "./getting.js"

export const importLabels = async (lang) => (await import(getLabelsUrl(lang))).default

export const importValidationErrors = async (lang) => (await import(getValidationErrorsUrl(lang))).default