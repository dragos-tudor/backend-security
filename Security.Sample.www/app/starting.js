import { getUserApi } from "../support/api/users.js"
import { createSetUserAction } from "../support/store/actions.js"
import { RoutePaths } from "../routes/paths.js"
const { navigate } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")

export const startApp = async (fetchApi, elem) =>
{
  const result = await getUserApi(fetchApi)
  const [user, error] = result
  if (error) return result

  dispatchAction(elem, createSetUserAction(user))
  navigate(elem, RoutePaths.home)
  return result
}