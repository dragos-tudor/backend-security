import { RoutePaths } from "../../support/route-paths/route.paths.js"
import { isLoginPath, isRootPath } from "../../support/route-paths/verifying.js"
import { getLocationPathName, getLocationPathNameAndSearch } from "../../support/locations/getting.js"
import { getRedirectedLogin } from "../../support/redirections/getting.js"
import { createSetUserAction } from "../../store/actions.js"
import { getUserApi } from "./getting.js"


export const startApp = async (fetchApi, dispatchAction, navigate, location = globalThis.location) =>
{
  const [user, error] = await getUserApi(fetchApi)
  if (error) isLoginPath(location)?
    navigate(getLocationPathNameAndSearch(location)):
    navigate(getRedirectedLogin(location))
  if (error) return [, error]

  dispatchAction(createSetUserAction(user))
  isLoginPath(location) || isRootPath(location)?
    navigate(RoutePaths.home):
    navigate(getLocationPathName(location))
  return [user]
}