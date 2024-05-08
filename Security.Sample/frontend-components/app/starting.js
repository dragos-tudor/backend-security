import { RoutePaths } from "../../frontend-shared/route-paths/route.paths.js"
import { isLoginPath, isRootPath } from "../../frontend-shared/route-paths/verifying.js"
import { getLocationPathName, getLocationPathNameAndSearch } from "../../frontend-shared/locations/getting.js"
import { getRedirectedLogin } from "../../frontend-shared/redirections/getting.js"
import { createAuthenticatedAction } from "../../frontend-shared/store/actions.js"
import { authenticatedAccountApi } from "../../frontend-proxy/mod.js"
import { isAuthenticationSuccedded } from "./verifying.js";

export const startApp = async (fetchApi, dispatchAction, navigate, location = globalThis.location) =>
{
  const [authenticated, error] = await authenticatedAccountApi(fetchApi)
  if (!isAuthenticationSuccedded(authenticated, error))
    isLoginPath(location)?
      navigate(getLocationPathNameAndSearch(location)):
      navigate(getRedirectedLogin(location))
  if (error) return [, error]
  if (!authenticated) return [authenticated]

  dispatchAction(createAuthenticatedAction(authenticated))
  isLoginPath(location) || isRootPath(location)?
    navigate(RoutePaths.home):
    navigate(getLocationPathName(location))
  return [authenticated]
}