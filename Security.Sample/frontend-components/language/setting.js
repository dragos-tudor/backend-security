import { Languages } from "../../frontend-shared/languages/languages.js"
import { LanguageParamName } from "../../frontend-shared/languages/param.name.js"
import { createLocationSearchParam } from "../../frontend-shared/locations/creating.js"
import { getLocationPathName } from "../../frontend-shared/locations/getting.js"

export const getPathWithEnglishParam = (location) =>
  getLocationPathName(location) + "?" + createLocationSearchParam(LanguageParamName, Languages.en)

export const getPathWithRomanianParam = (location) =>
  getLocationPathName(location) + "?" + createLocationSearchParam(LanguageParamName, Languages.ro)