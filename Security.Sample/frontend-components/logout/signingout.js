import { createSetUserAction, createAuthenticatedAction } from "../../frontend-shared/store/actions.js"
import { RoutePaths } from "../../frontend-shared/route-paths/route.paths.js"
import { signOutAccoutApi } from "../../frontend-proxy/mod.js"
import { isSignoutError } from "./verifying.js";

export const signOutAccount = async (fetchApi, dispatchAction, navigate) =>
{
  const [, error] = await signOutAccoutApi(fetchApi)
  if (isSignoutError(error)) return [, error]

  dispatchAction(createSetUserAction(null))
  dispatchAction(createAuthenticatedAction(false))
  navigate(RoutePaths.login)
  return [true]
}