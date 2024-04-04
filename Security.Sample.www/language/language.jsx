import { Languages } from "../support/languages/languages.js"
import { useLanguage } from "../support/services/using.js"
import { getPathWithEnglishParam, getPathWithRomanianParam } from "./setting.js"
import { isEnglishLanguage, isRomanianLanguage } from "./verifying.js"
const { useLocation } = await import ("/scripts/routing.js")

export const Language = (_, elem) =>
{
  const lang = useLanguage(elem)
  const location = useLocation(elem)

  return <>
    <a href={(location && getPathWithEnglishParam(location))} hidden={isEnglishLanguage(lang)} target="_self">
      {Languages.en}
    </a>
    <a href={(location && getPathWithRomanianParam(location))} hidden={isRomanianLanguage(lang)} target="_self">
      {Languages.ro}
    </a>
  </>
}