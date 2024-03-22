import { ServiceNames } from "../support/services/names.js"

export const createAppProps = (fetchApi, apiUrl) => Object.freeze({
  [ServiceNames.fetchApi]: fetchApi,
  [ServiceNames.apiUrl]: apiUrl
})