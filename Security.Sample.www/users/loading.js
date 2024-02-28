import { getUser } from "./getting.js"
import { navigateUser } from "./navigating.js"
const { update }  = await import("../scripts/rendering.js")

export const loadUser = async (apiFetch, setUser, elem) => {
  const user = await getUser(apiFetch)
  setUser(user)
  update(elem)
  return navigateUser(elem, user)
}