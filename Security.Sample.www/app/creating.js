import { ServiceNames } from "../support/services/names.js"

export const createAppProps = (settings, fetchApi, labels, validationErrors) => Object.freeze({
  [ServiceNames.apiUrl]: settings.apiUrl,
  [ServiceNames.fetchApi]: fetchApi,
  [ServiceNames.labels]: labels,
  [ServiceNames.validationErrors]: validationErrors,
  errorTimeout: settings.errorTimeout
})