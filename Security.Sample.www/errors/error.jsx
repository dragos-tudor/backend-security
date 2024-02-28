import { getErrorDescription, getErrorName } from "./getting.js"
const { NavLink } = await import("../scripts/routing.js")

export const Error = ({location}) =>
  <>
    <h3>Error</h3>
    <p>An error occurred while processing your request.</p>

    <h4>{getErrorName(location ?? globalThis.location)}</h4>
    <p>{getErrorDescription(location ?? globalThis.location)}</p>

    <NavLink href="/home">Back to home</NavLink>
  </>

