import { getLabelsPath } from "./getting.js"

export const importLabels = async (lang) => (await import(getLabelsPath(lang))).default