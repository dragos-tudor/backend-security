import { ServiceNames } from "./services.names.js"
const { getService } = await import("/scripts/rendering.js")

export const useApiUrl = (elem) => getService(elem, ServiceNames.apiUrl)

export const useFetchApi = (elem) => getService(elem, ServiceNames.fetchApi)

export const useLabels = (elem) => getService(elem, ServiceNames.labels)

export const useLanguage = (elem) => getService(elem, ServiceNames.language)

export const useValidationErrors = (elem) => getService(elem, ServiceNames.validationErrors)
