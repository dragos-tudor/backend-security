import { navigate, update } from "../deps.js"
import { routes } from "../routes/routes.jsx"
import { getUser } from "./getting.js"

export const loadUser = async (fetchApi, setUser, elem) =>
{
  const [user, error] = await getUser(fetchApi)
  if (error) return navigate(elem, routes.login)

  setUser(user)
  update(elem)
  return navigate(elem, routes.home)
}