import { getRedirectParam } from "../../frontend-shared/redirections/getting.js"
import { hasLocationRedirect } from "../../frontend-shared/redirections/verifying.js"
import { RoutePaths } from "../../frontend-shared/route-paths/route.paths.js"
import { createAuthenticatedAction } from "../../frontend-shared/store/actions.js"
import { signInAccountApi } from "../../frontend-proxy/mod.js"
import { IsUnauthorizedError } from "./verifying.js";

export const signInAccount = async (credentials, fetchApi, dispatchAction, navigate, sendError, labels, location) =>
{
  const [_, error] = await signInAccountApi(credentials, fetchApi)
  if (IsUnauthorizedError(error)) sendError(labels.wrongCredentials)
  if (error) return [, error]

  dispatchAction(createAuthenticatedAction(true))
  hasLocationRedirect(location)?
    navigate(getRedirectParam(location)):
    navigate(RoutePaths.home)
  return [true]
}