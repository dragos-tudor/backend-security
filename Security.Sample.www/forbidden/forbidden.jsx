import { getLabels } from "../support/services/getting.js"
import { getErrorDescription } from "./getting.js"
const { NavLink } = await import("../scripts/routing.js")

export const Forbidden = ({location}) => {
  const labels = getLabels(elem)
  return <>
    <style css={css}></style>
    <h3>{labels["accessDenied"]}</h3>

    <p>{labels["unauthorized"]}</p>
    <p>{getErrorDescription(location ?? globalThis.location)}</p>

    <NavLink href="/home">{labels["gotoHome"]}</NavLink>
  </>
}

const css = `
forbidden {
  display: block;
  margin: 3rem;
}

forbidden h3 {
  color: var(--error-color)
}`