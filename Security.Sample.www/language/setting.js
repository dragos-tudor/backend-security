import { Languages } from "../support/languages/languages.js"
import { LanguageParamName } from "../support/languages/param.name.js"
import { createLocationSearchParam } from "../support/locations/creating.js"
import { getLocationPathName } from "../support/locations/getting.js"

export const getPathWithEnglishParam = (location) =>
  getLocationPathName(location) + "?" + createLocationSearchParam(LanguageParamName, Languages.en)

export const getPathWithRomanianParam = (location) =>
  getLocationPathName(location) + "?" + createLocationSearchParam(LanguageParamName, Languages.ro)