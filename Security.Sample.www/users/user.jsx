import { useEffect, useState, Context } from "../deps.js"
import { getFetchApi } from "../services/getting.js"
import { loadUser } from "./loading.js"

export const User = (props, elem) =>
{
  const [user, setUser] = useState(elem, "user", null, [])
  const fetchApi = getFetchApi(elem)

  useEffect(elem, "user", () => loadUser(fetchApi, setUser, elem), [])
  return (
    <Context name="user" value={user}>
      {props.children}
    </Context>)
}