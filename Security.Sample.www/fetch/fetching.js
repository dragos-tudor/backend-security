import { fetchJson } from "../deps.js"
import { routes } from "../routes/routes.jsx"
import { getAbortSignal, getResponseLocation } from "./getting.js"
import { logResponseError } from "./logging.js"
import { setFetchRedirect, setFetchSignal } from "./setting.js"
import { isResponseUnauthorized, isResponseForbidden, isResponseRedirect } from "./verifying.js"


export const getFetchJson = (fetch, navigate, timeout = 2000) => async (url, data, request = {}) =>
{
  const { signal, timeoutId } = getAbortSignal(timeout)
  setFetchRedirect(request, "manual")
  setFetchSignal(request, signal)
  const result = await fetchJson(fetch, url, data, request)
  const error = result[1]

  clearTimeout(timeoutId)
  if (error) logResponseError(error.type, error)
  if (isResponseForbidden(error)) navigate(routes.accessdenied)
  if (isResponseRedirect(error)) navigate(getResponseLocation(response))
  if (isResponseUnauthorized(error)) navigate(routes.login)

  return result
}