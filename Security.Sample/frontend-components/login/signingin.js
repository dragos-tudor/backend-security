import { getRedirectParam } from "../../frontend-shared/redirections/getting.js"
import { hasLocationRedirect } from "../../frontend-shared/redirections/verifying.js"
import { RoutePaths } from "../../frontend-shared/route-paths/route.paths.js"
import { createAuthenticatedAction } from "../../frontend-shared/store/actions.js"
import { signInAccountApi } from "../../frontend-proxy/mod.js"
import { isUnauthorizedError } from "./verifying.js";
import { isSigninError } from "./verifying.js";

export const signInAccount = async (credentials, fetchApi, dispatchAction, navigate, sendError, labels, location) =>
{
  const [_, error] = await signInAccountApi(credentials, fetchApi)
  if (isUnauthorizedError(error)) sendError(labels.wrongCredentials)
  if (isSigninError(error)) return [, error]

  dispatchAction(createAuthenticatedAction(true))
  hasLocationRedirect(location)?
    navigate(getRedirectParam(location)):
    navigate(RoutePaths.home)
  return [true]
}