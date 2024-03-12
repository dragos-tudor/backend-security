import { getApiFetchService } from "../services/getting.js"
import { useEffect, useState } from "../scripts/extending.js"
import { loadUser } from "./loading.js"
const { Context }  = await import("/scripts/rendering.js")

export const User = (props, elem) =>
{
  const apiFetch = getApiFetchService(elem)
  const [user, setUser] = useState(elem, "user", null, [])
  useEffect(elem, "userEffect", () => loadUser(apiFetch, setUser, elem), [])

  return (
    <Context name="user" value={user}>
      {props.children}
    </Context>)
}