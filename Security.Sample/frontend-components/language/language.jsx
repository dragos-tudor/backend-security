import { Languages } from "../../frontend-shared/languages/languages.js"
import { isEnglishLanguage, isRomanianLanguage } from "../../frontend-shared/languages/verifying.js"
import { useLanguage } from "../../frontend-shared/services/using.js"
import { getPathWithEnglishParam, getPathWithRomanianParam } from "./setting.js"
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