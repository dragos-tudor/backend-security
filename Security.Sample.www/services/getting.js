import { getService } from "../deps.js"
import { services } from "./services.js"

export const getFetchApi = (elem) =>
  getService(elem, services.fetchApi)

export const getApiUrl = (elem) =>
  getService(elem, services.apiUrl)