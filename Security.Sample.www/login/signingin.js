import { signInAccountApi } from "../support/api/accounts.js"
import { createSetUserAction } from "../support/store/actions.js"
import { RoutePaths } from "../routes/paths.js"
const { update } = await import("/scripts/rendering.js")
const { navigate } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")

export const signInClick = async (credentials, fetchApi, setSigning, elem) =>
  setSigning(true) &&
  update(elem) &&
  await signInUser(credentials, fetchApi, elem) &&
  setSigning(false) ||
  update(elem)


export const signInUser = async (credentials, fetchApi, elem) =>
{
  const [user, error] = await signInAccountApi(credentials, fetchApi)
  if (error) return [, error]

  dispatchAction(elem, createSetUserAction(user))
  navigate(elem, RoutePaths.home)
  return [user]
}