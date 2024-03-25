import { getSearchParam } from "../support/locations/getting.js"

export const getLabelsUrl = (lang) => `/scripts/labels.${lang}.js`

export const getLangSearchParam = (location) => getSearchParam(location, "lang")

export const getValidationErrorsUrl = (lang) => `/scripts/validations.${lang}.js`