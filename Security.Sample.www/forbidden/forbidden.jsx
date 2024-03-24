import { getErrorDescription } from "./getting.js"
const { NavLink } = await import("/scripts/routing.js")

export const Forbidden = ({location}) =>
  <>
    <style css={css}></style>
    <h3>Access denied</h3>

    <p>You are not authorize to access resource.</p>
    <p>{getErrorDescription(location ?? globalThis.location)}</p>

    <NavLink href="/home">Go to home</NavLink>
  </>

const css = `
forbidden {
  display: block;
  margin: 3rem;
}

forbidden h3 {
  color: var(--error-color)
}`