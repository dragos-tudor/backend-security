import { navigateToAccessDenied, navigateToLogin } from "../router/navigating.js"
const { expBackoff, fetchJson } = await import("/scripts/fetching.js")

const apiFetch = (baseUrl, fetch, router) => async (url, data, request) => {
  const result = await fetchJson(fetch, baseUrl + url, data, request)
  const success = result[0]
  if (success?.status === 401) navigateToLogin(router)
  if (success?.status === 403) navigateToAccessDenied(router)
  return result
}

const resilientFetch = (intervals, fetch) =>
  (url, data, request) => expBackoff(intervals, fetch, url, data, request)

export const resilientApiFetch = (settings, fetch, document) =>
  resilientFetch(
    settings.expBackoff?.intervals,
    apiFetch(settings.apiUrl, fetch, getAppRouter(document))
  )

