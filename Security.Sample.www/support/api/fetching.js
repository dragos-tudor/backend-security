import { setLoginRedirectUrl } from "../../redirections/setting.js"
import { RoutePaths } from "../../routes/paths.js"
import { setRequestCredentials, setRequestMode, setRequestRedirect } from "./setting.js"
import { isUnauthorizedResponse, isForbiddenResponse } from "./verifying.js"
const { fetchJson } = await import("/scripts/fetching.js")

export const getFetchApi = (fetch, navigate, handle, location) => async (url, request = {}) =>
{
  setRequestMode(request, "cors")
  setRequestRedirect(request, "manual")
  setRequestCredentials(request, "include")
  const [data, error] = await fetchJson(fetch, url, request)

  if (!error) return [data]
  if (error) handle(error)
  if (isUnauthorizedResponse(error.response)) navigate(setLoginRedirectUrl(location))
  if (isForbiddenResponse(error.response)) navigate(RoutePaths.forbidden)

  return [, error]
}


