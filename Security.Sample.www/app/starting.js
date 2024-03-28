import { isLoginPath, isRootPath } from "../routes/verifying.js"
import { getUserApi } from "../support/api/users.js"
import { createSetUserAction } from "../support/store/actions.js"
const { navigate } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")

export const startApp = async (fetchApi, location, elem) =>
{
  const [user, error] = await getUserApi(fetchApi)
  if (error) return [, error]

  dispatchAction(elem, createSetUserAction(user))
  isLoginPath(location) || isRootPath(location)?
    navigate(elem, RoutePaths.home):
    navigate(elem, getLocationPathName(location))
  return [user]
}