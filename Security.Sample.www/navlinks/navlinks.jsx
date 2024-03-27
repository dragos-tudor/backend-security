import { useLabels } from "../support/services/using.js"
import { Logout } from "../logout/logout.jsx"
import { selectUserAuthenticated } from "../support/store/selectors.js"
import { useSelector } from "../scripts/extending.js"
const { NavLink } = await import("/scripts/routing.js")


export const NavLinks = (props, elem) =>
{
  const authenticated = useSelector(elem, "authenticated", selectUserAuthenticated)
  const labels = useLabels(elem)
  return <>
    <style css={css}></style>
    {
      authenticated?
        <nav>
          <NavLink href="/home">{labels["home"]}</NavLink>
          <Logout></Logout>
        </nav>:
        <nav>
          <NavLink href="/login">{labels["login"]}</NavLink>
        </nav>
    }
  </>
}

const css = `
navlinks {
  margin: 0 2rem;
}`