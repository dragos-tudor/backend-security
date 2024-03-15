import { NavLink } from "../deps.js"
import { getErrorDescription } from "./getting.js"

export const AccessDenied = ({location}) =>
  <>
    <style css={css}></style>
    <h3>Access denied</h3>

    <p>You are not authorize to access resource.</p>
    <p>{getErrorDescription(location ?? globalThis.location)}</p>

    <NavLink href="/login">Go to login</NavLink>
  </>
const css = `
access-denied {
  display: block;
  margin: 3rem;
}

access-denied h3 {
  color: var(--error-color)
}`