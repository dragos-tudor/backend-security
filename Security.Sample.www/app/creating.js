import { ServiceNames } from "../support/services/names.js"

export const createAppProps = (apiUrl, fetchApi, labels, validationErrors) => Object.freeze({
  [ServiceNames.apiUrl]: apiUrl,
  [ServiceNames.fetchApi]: fetchApi,
  [ServiceNames.labels]: labels,
  [ServiceNames.validationErrors]: validationErrors
})