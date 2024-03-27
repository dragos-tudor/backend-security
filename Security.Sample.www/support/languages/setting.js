import { LanguageParamName } from "./getting.js"

export const setLanguageParam = (url, lang) =>
{
  url.searchParams.set(LanguageParamName, lang)
  return url.href
}
