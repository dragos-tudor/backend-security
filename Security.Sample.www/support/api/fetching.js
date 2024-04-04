import { getRedirectedLogin } from "../redirections/getting.js"
import { RoutePaths } from "../../routes/paths.js"
import { isLoginPath } from "../../routes/verifying.js"
import { setRequestCredentials, setRequestMode, setRequestRedirect } from "./setting.js"
import { isUnauthorizedResponse, isForbiddenResponse } from "./verifying.js"
const { fetchJson } = await import("/scripts/fetching.js")

export const getFetchApi = (fetch, navigate, handleError, location = globalThis.location) => async (url, request = {}) =>
{
  setRequestMode(request, "cors")
  setRequestRedirect(request, "manual")
  setRequestCredentials(request, "include")
  const [data, error] = await fetchJson(fetch, url, request)

  if (!error) return [data]
  if (error) handleError(error)
  if (isUnauthorizedResponse(error.response)) !isLoginPath(location) && navigate(getRedirectedLogin(location))
  if (isForbiddenResponse(error.response)) navigate(RoutePaths.forbidden)

  return [, error]
}


