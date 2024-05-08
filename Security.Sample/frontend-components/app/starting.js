import { RoutePaths } from "../../frontend-shared/route-paths/route.paths.js"
import { isLoginPath, isRootPath } from "../../frontend-shared/route-paths/verifying.js"
import { getLocationPathName, getLocationPathNameAndSearch } from "../../frontend-shared/locations/getting.js"
import { getRedirectedLogin } from "../../frontend-shared/redirections/getting.js"
import { createIsAuthenticatedAction } from "../../frontend-shared/store/actions.js"
import { isAuthenticatedAccountApi } from "../../frontend-proxy/mod.js"
import { isAuthenticationSuccedded } from "./verifying.js";

export const startApp = async (fetchApi, dispatchAction, navigate, location = globalThis.location) =>
{
  const [isAuthenticated, error] = await isAuthenticatedAccountApi(fetchApi)
  if (!isAuthenticationSuccedded(isAuthenticated, error))
    isLoginPath(location)?
      navigate(getLocationPathNameAndSearch(location)):
      navigate(getRedirectedLogin(location))
  if (error) return [, error]
  if (!isAuthenticated) return [isAuthenticated]

  dispatchAction(createIsAuthenticatedAction(isAuthenticated))
  isLoginPath(location) || isRootPath(location)?
    navigate(RoutePaths.home):
    navigate(getLocationPathName(location))
  return [isAuthenticated]
}