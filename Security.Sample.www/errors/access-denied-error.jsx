import { getErrorDescription } from "./getting.js"
const { NavLink } = await import("../scripts/routing.js")

export const AccessDeniedError = ({location}) =>
  <>
    <h3>Access denied</h3>
    <p>You are not authorize to access resource.</p>
    <p>{getErrorDescription(location ?? globalThis.location)}</p>
    <NavLink href="/login">Go to login</NavLink>
  </>