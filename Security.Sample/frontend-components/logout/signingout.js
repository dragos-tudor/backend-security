import { createSetUserAction, createIsAuthenticatedAction } from "../../frontend-shared/store/actions.js"
import { RoutePaths } from "../../frontend-shared/route-paths/route.paths.js"
import { signOutAccoutApi } from "../../frontend-proxy/mod.js"

export const signOutAccount = async (fetchApi, dispatchAction, navigate) =>
{
  const [, error] = await signOutAccoutApi(fetchApi)
  if (error) return [, error]

  dispatchAction(createSetUserAction(null))
  dispatchAction(createIsAuthenticatedAction(false))
  navigate(RoutePaths.login)
  return [true]
}