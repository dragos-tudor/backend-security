import { useEffect } from "../extensions/effects.js"
import { useState } from "../extensions/states.js"
import { loadUser } from "./loading.js"
const { getService, Context }  = await import("/scripts/rendering.js")

export const User = (props, elem) =>
{
  const apiFetch = getService(elem, "api-fetch")
  const [user, setUser] = useState(elem, "userState", null, [])

  useEffect(elem, "userEffect", () => loadUser(apiFetch, setUser, elem), [])
  return (
    <Context name="user" value={user}>
      {props.children}
    </Context>)
}