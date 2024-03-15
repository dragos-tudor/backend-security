import { navigate } from "../deps.js"
import { getFetchJson } from "../fetch/fetching.js"
import { services } from "../services/services.js"

export const createAppProps = (settings, router) => Object.freeze({
  [services.fetchApi]: getFetchJson(
    (url, request) => fetch(settings.apiUrl + url, request),
    (url) => navigate(router, url),
    3000
  ),
  [services.apiUrl]: settings.apiUrl
})