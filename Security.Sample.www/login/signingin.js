import { getRedirectParam } from "../support/redirections/getting.js"
import { hasLocationRedirect } from "../support/redirections/verifying.js"
import { RoutePaths } from "../routes/paths.js"
import { signInAccountApi } from "../support/api/accounts.js"
import { createSetUserAction } from "../support/store/actions.js"
const { navigate } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")

export const signInUser = async (credentials, location, fetchApi, elem) =>
{
  const [user, error] = await signInAccountApi(credentials, fetchApi)
  if (error) return [, error]

  dispatchAction(elem, createSetUserAction(user))
  hasLocationRedirect(location)?
    navigate(elem, getRedirectParam(location)):
    navigate(elem, RoutePaths.home)
  return [user]
}