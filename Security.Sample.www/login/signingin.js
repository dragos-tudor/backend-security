import { getLocationRedirect } from "../redirections/getting.js"
import { hasLocationRedirect } from "../redirections/verifying.js"
import { RoutePaths } from "../routes/paths.js"
import { signInAccountApi } from "../support/api/accounts.js"
import { createSetUserAction } from "../support/store/actions.js"
const { update } = await import("/scripts/rendering.js")
const { navigate } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")


export const signInClick = async (credentials, location, fetchApi, setSigning, elem) =>
  setSigning(true) &&
  update(elem) &&
  await signInUser(credentials, location, fetchApi, elem) &&
  setSigning(false) ||
  update(elem)

export const signInUser = async (credentials, location, fetchApi, elem) =>
{
  const [user, error] = await signInAccountApi(credentials, fetchApi)
  if (error) return [, error]

  dispatchAction(elem, createSetUserAction(user))
  hasLocationRedirect(location)?
    navigate(elem, getLocationRedirect(location)):
    navigate(elem, RoutePaths.home)
  return [user]
}