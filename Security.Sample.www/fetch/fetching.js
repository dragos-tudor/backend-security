import { navigateToAccessDenied, navigateToLogin } from "../router/navigating.js"
import { setFetchRedirect } from "./setting.js"
const { fetchJson } = await import("/scripts/fetching.js")

export const apiFetch = (baseUrl, fetch, router) => async (url, data, request) => {
  const requestInit = setFetchRedirect(request, "manual")
  const result = await fetchJson(fetch, baseUrl + url, data, requestInit)
  const success = result[0]
  if (success?.status === 401) navigateToLogin(router)
  if (success?.status === 403) navigateToAccessDenied(router)
  return result
}


