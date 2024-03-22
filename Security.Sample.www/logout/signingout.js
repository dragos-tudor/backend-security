import { signOutAccoutApi } from "../support/api/accounts.js"
import { createSetUserAction } from "../support/store/actions.js"
import { RoutePaths } from "../routes/paths.js"
const { navigate } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")

export const signOutUser = async (fetchApi, elem) =>
{
  const result = await signOutAccoutApi(fetchApi)
  const [, error] = result
  if (error) return result

  dispatchAction(elem, createSetUserAction(null))
  navigate(elem, RoutePaths.login)

  return result
}