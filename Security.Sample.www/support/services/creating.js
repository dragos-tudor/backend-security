import { ServiceNames } from "./names.js"

export const createServices = (apiUrl, fetchApi, language, labels, validationErrors) => Object.freeze({
  [ServiceNames.apiUrl]: apiUrl,
  [ServiceNames.fetchApi]: fetchApi,
  [ServiceNames.labels]: labels,
  [ServiceNames.language]: language,
  [ServiceNames.validationErrors]: validationErrors
})