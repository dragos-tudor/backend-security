import { getRedirectedLogin } from "../redirections/getting.js"
import { RoutePaths } from "../route-paths/route.paths.js"
import { isLoginPath } from "../route-paths/verifying.js"
import { setRequestCredentials, setRequestMode, setRequestRedirect } from "../requests/setting.js"
import { isUnauthorizedResponse, isForbiddenResponse } from "../responses/verifying.js"
import { isFetchSuccess, isFetchFailure } from "./verifying.js"
const { fetchJson } = await import("/scripts/fetching.js")

export const getFetchApi = (fetch, navigate, sendError, location = globalThis.location) => async (url, request = {}) =>
{
  setRequestMode(request, "cors")
  setRequestRedirect(request, "manual")
  setRequestCredentials(request, "include")
  const [data, error] = await fetchJson(fetch, url, request)

  if (isFetchSuccess(error)) return [data]
  if (isFetchFailure(error)) sendError(error)
  if (isUnauthorizedResponse(error.response)) !isLoginPath(location) && navigate(getRedirectedLogin(location))
  if (isForbiddenResponse(error.response)) navigate(RoutePaths.forbidden)

  return [, error]
}


