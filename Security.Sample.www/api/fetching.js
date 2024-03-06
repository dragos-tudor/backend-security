const { expBackoff, fetchJson } = await import("/scripts/fetching.js")

export const apiFetch = (baseUrl, fetch, navigateToAccessDenied, navigateToLogin) => async (url, data, request) => {
  const result = await fetchJson(fetch, baseUrl + url, data, request)
  const success = result[0]
  if (success?.status === 401) return navigateToLogin()
  if (success?.status === 403) return navigateToAccessDenied()
  return result
}

export const resilientFetch = (intervals, fetch) =>
  (url, data, request) => expBackoff(intervals, fetch, url, data, request)