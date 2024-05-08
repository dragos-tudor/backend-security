import { resolveLocation } from "../../frontend-shared/locations/resolving.js"
import { useLabels } from "../../frontend-shared/services/using.js"
import { getErrorDescription } from "./getting.js"
const { NavLink } = await import("/scripts/routing.js")

export const Forbidden = (props) =>
{
  const labels = useLabels(elem)
  const location = resolveLocation(props.location)
  const error = getErrorDescription(location)

  return <>
    <style css={css}></style>
    <h3>{labels["accessDenied"]}</h3>

    <p>{labels["unauthorized"]}</p>
    <p>{error}</p>

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