import { isEnglishLanguage } from "../../language/verifying.js"
import { importLabels } from "./importing.js"
import { Labels } from "./labels.js"

export const resolveLabels = async (lang) =>
  isEnglishLanguage(lang)? Labels: await importLabels(lang)