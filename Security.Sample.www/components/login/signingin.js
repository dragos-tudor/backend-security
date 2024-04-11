import { getRedirectParam } from "../../support/redirections/getting.js"
import { hasLocationRedirect } from "../../support/redirections/verifying.js"
import { RoutePaths } from "../../support/route-paths/route.paths.js"
import { createSetUserAction } from "../../store/actions.js"
const { HttpMethods } = await import("/scripts/fetching.js")


export const signInAccountApi = (credentials, fetchApi) =>
  fetchApi("/accounts/signin", { method: HttpMethods.POST, body: credentials })

export const signInAccount = async (credentials, location, fetchApi, dispatchAction, navigate) =>
{
  const [user, error] = await signInAccountApi(credentials, fetchApi)
  if (error) return [, error]

  dispatchAction(createSetUserAction(user))
  hasLocationRedirect(location)?
    navigate(getRedirectParam(location)):
    navigate(RoutePaths.home)
  return [user]
}