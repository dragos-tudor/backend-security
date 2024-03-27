import { Languages } from "../support/languages/languages.js"
import { setLanguageParam } from "../support/languages/setting.js"
import { isEnglishLanguage, isRomanianLanguage } from "../support/languages/verifying.js";
import { toUrl } from "../support/locations/converting.js";
import { getLocation } from "../support/locations/getting.js";
import { getLanguage } from "../support/services/getting.js"


export const Language = (props, elem) =>
{
  const lang = getLanguage(elem)
  const url = toUrl(getLocation(props.location))
  return <>
    <a href={setLanguageParam(url, Languages.en)} hidden={isEnglishLanguage(lang)} target="_self">
      {Languages.en}
    </a>
    <a href={setLanguageParam(url, Languages.ro)} hidden={isRomanianLanguage(lang)} target="_self">
      {Languages.ro}
    </a>
  </>
}