import { RoutePaths } from "../routes/paths.js"
import { isLoginPath, isRootPath } from "../routes/verifying.js"
import { getUserApi } from "../support/api/users.js"
import { getLocationPathName, getLocationPathNameAndSearch } from "../support/locations/getting.js"
import { getRedirectedLogin } from "../support/redirections/getting.js"
import { createSetUserAction } from "../support/store/actions.js"


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