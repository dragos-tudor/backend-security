import { getRedirectParam } from "../../frontend-shared/redirections/getting.js"
import { hasLocationRedirect } from "../../frontend-shared/redirections/verifying.js"
import { RoutePaths } from "../../frontend-shared/route-paths/route.paths.js"
import { createIsAuthenticatedAction } from "../../frontend-shared/store/actions.js"
import { signInAccountApi } from "../../frontend-proxy/mod.js"

export const signInAccount = async (credentials, location, fetchApi, dispatchAction, navigate) =>
{
  const [, error] = await signInAccountApi(credentials, fetchApi)
  if (error) return [, error]

  dispatchAction(createIsAuthenticatedAction(true))
  hasLocationRedirect(location)?
    navigate(getRedirectParam(location)):
    navigate(RoutePaths.home)
  return [true]
}