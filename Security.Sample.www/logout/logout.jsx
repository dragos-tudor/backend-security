import { getFetchApi } from "../services/getting.js"
import { signoutUser } from "./signingout.js"

export const Logout = (_, elem) =>
{
  const fetchApi = getFetchApi(elem)
  return (
    <button onclick={signoutUser(fetchApi)}>
      Signout
    </button>)
}
