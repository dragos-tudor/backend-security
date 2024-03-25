import { getLabel } from "../languages/labels.js"
import { getErrorDescription } from "./getting.js"
const { NavLink } = await import("/scripts/routing.js")

export const Forbidden = ({location}) =>
  <>
    <style css={css}></style>
    <h3>{getLabel("accessDenied")}</h3>

    <p>{getLabel("unauthorized")}</p>
    <p>{getErrorDescription(location ?? globalThis.location)}</p>

    <NavLink href="/home">{getLabel("gotoHome")}</NavLink>
  </>

const css = `
forbidden {
  display: block;
  margin: 3rem;
}

forbidden h3 {
  color: var(--error-color)
}`