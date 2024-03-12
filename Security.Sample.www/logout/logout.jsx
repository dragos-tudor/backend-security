import { getApiFetchService } from "../services/getting.js"
import { signoutUser } from "./signingout.js"
const { NavLink } = await import("/scripts/routing.js")

export const Logout = (_, elem) => {
  const apiFetch = getApiFetchService(elem)

  return (
    <NavLink href="/" onclick={signoutUser(apiFetch)}>
      Signout
    </NavLink>)
}
