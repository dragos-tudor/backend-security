import { ServiceNames } from "./names.js"
const { getService } = await import("../../scripts/rendering.js")

export const getApiUrl = (elem) => getService(elem, ServiceNames.apiUrl)

export const getFetchApi = (elem) => getService(elem, ServiceNames.fetchApi)

export const getLabels = (elem) => getService(elem, ServiceNames.labels)

export const getLanguage = (elem) => getService(elem, ServiceNames.language)

export const getValidationErrors = (elem) => getService(elem, ServiceNames.validationErrors)
