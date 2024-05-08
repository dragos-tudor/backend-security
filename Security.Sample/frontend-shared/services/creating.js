import { ServiceNames } from "./services.names.js"

export const createServices = (apiUrl, fetchApi, labels, language, validationErrors) => Object.freeze({
  [ServiceNames.apiUrl]: apiUrl,
  [ServiceNames.fetchApi]: fetchApi,
  [ServiceNames.labels]: labels,
  [ServiceNames.language]: language,
  [ServiceNames.validationErrors]: validationErrors
})