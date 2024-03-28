import { toUrl } from "../support/locations/converting.js"
import { resolveLocation } from "../support/locations/resolving.js"
import { Languages } from "../support/languages/languages.js"
import { useLanguage } from "../support/services/using.js"
import { setEnglishParam, setRomanianParam } from "./setting.js"
import { isEnglishLanguage, isRomanianLanguage } from "./verifying.js"
const { useLocation } = await import("/scripts/routing.js")

export const Language = (_, elem) =>
{
  const lang = useLanguage(elem)
  const location = useLocation(elem)
  const url = toUrl(resolveLocation(location))
  const searchParams = url.searchParams

  return <>
    <a href={(setEnglishParam(searchParams), url.href)} hidden={isEnglishLanguage(lang)} target="_self">
      {Languages.en}
    </a>
    <a href={(setRomanianParam(searchParams), url.href)} hidden={isRomanianLanguage(lang)} target="_self">
      {Languages.ro}
    </a>
  </>
}