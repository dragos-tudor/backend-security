import { navigate } from "../deps.js";
import { logResponseError } from "../fetch/logging.js"
import { routes } from "../routes/routes.jsx"

export const signoutUser = (fetchApi, elem) => async () =>
{
  const result = await fetchApi("/accounts/signout", null, { method: "POST" })
  const error = result[1]
  if (error) logResponseError(error)

  navigate(elem, routes.login)
}