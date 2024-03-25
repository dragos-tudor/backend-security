import { getUserApi } from "../support/api/users.js"
import { createSetUserAction } from "../support/store/actions.js"
import { RoutePaths } from "../routes/paths.js"
const { navigate } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")

export const startApp = async (fetchApi, elem) =>
{
  const [user, error] = await getUserApi(fetchApi)
  if (error) navigate(elem, RoutePaths.login)
  if (error) return [, error]

  dispatchAction(elem, createSetUserAction(user))
  navigate(elem, RoutePaths.home)
  return [user]
}