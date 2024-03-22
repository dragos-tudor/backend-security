import { ServiceNames } from "./names.js"
const { getService } = await import("/scripts/rendering.js")

export const getFetchApiService = (elem) =>
  getService(elem, ServiceNames.fetchApi)

export const getApiUrlService = (elem) =>
  getService(elem, ServiceNames.apiUrl)