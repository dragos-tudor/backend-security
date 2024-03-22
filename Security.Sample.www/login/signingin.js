import { signInAccountApi } from "../support/api/accounts.js"
import { createSetUserAction } from "../support/store/actions.js"
import { RoutePaths } from "../routes/paths.js"
const { navigate } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")

export const signInUser = async (credentials, fetchApi, elem) =>
{
  const result = await signInAccountApi(credentials, fetchApi)
  const [user, error] = result
  if (error) return result

  dispatchAction(elem, createSetUserAction(user))
  navigate(elem, RoutePaths.home)

  return result
}