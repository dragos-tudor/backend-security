import { RoutePaths } from "../../routes/paths.js"
import { logResponseError } from "../responses/logging.js"
import { setRequestCredentials, setRequestMode, setRequestRedirect } from "../requests/setting.js"
import { isUnauthorizedResponse, isForbiddenResponse } from "../responses/verifying.js"
const { fetchJson } = await import("/scripts/fetching.js")

export const getFetchApi = (fetch, navigate, logger = () => {}) => async (url, request = {}) =>
{
  setRequestMode(request, "cors")
  setRequestRedirect(request, "manual")
  setRequestCredentials(request, "include")

  const result = await fetchJson(fetch, url, request)
  const [_, error] = result

  if (!error) return result
  if (error) logResponseError(error, logger)
  if (isUnauthorizedResponse(error.response)) navigate(RoutePaths.login)
  if (isForbiddenResponse(error.response)) navigate(RoutePaths.accessdenied)

  return result
}


