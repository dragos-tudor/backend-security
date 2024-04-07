import { getRedirectParam } from "../support/redirections/getting.js"
import { hasLocationRedirect } from "../support/redirections/verifying.js"
import { RoutePaths } from "../routes/paths.js"
import { signInAccountApi } from "../support/api/accounts.js"
import { createSetUserAction } from "../support/store/actions.js"

export const signInUser = async (credentials, location, fetchApi, dispatchAction, navigate) =>
{
  const [user, error] = await signInAccountApi(credentials, fetchApi)
  if (error) return [, error]

  dispatchAction(createSetUserAction(user))
  hasLocationRedirect(location)?
    navigate(getRedirectParam(location)):
    navigate(RoutePaths.home)
  return [user]
}