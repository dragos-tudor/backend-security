import { signOutAccoutApi } from "../support/api/accounts.js"
import { createSetUserAction } from "../support/store/actions.js"
import { RoutePaths } from "../routes/paths.js"
const { navigate } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")

export const signOutUser = async (fetchApi, elem) =>
{
  const [_, error] = await signOutAccoutApi(fetchApi)
  if (error) return [, error]

  dispatchAction(elem, createSetUserAction(null))
  navigate(elem, RoutePaths.login)
  return [_]
}