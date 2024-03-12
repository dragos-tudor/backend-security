import { getUser } from "./getting.js"
const { update }  = await import("/scripts/rendering.js")
const { navigate } = await import("/scripts/routing.js")

export const loadUser = async (apiFetch, setUser, elem) => {
  const [user, failure] = await getUser(apiFetch)
  if (failure) return navigate(elem, "/login")

  setUser(user)
  update(elem)
  return navigate(elem, "/home")
}