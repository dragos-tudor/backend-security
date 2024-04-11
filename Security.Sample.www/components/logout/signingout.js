import { createSetUserAction } from "../../store/actions.js"
import { RoutePaths } from "../../support/route-paths/route.paths.js"
const { navigate } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")
const { HttpMethods } = await import("/scripts/fetching.js")


export const signOutAccoutApi = (fetchApi)  =>
  fetchApi("/accounts/signout", { method: HttpMethods.POST })

export const signOutAccount = async (fetchApi, elem) =>
{
  const [_, error] = await signOutAccoutApi(fetchApi)
  if (error) return [, error]

  dispatchAction(elem, createSetUserAction(null))
  navigate(elem, RoutePaths.login)
  return [_]
}