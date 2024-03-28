import { useLabels } from "../support/services/using.js"
import { Logout } from "../logout/logout.jsx"
import { RoutePaths } from "../routes/paths.js"
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
          <NavLink href={RoutePaths.home}>{labels["home"]}</NavLink>
          <NavLink href={RoutePaths.info}>{labels["info"]}</NavLink>
          <Logout></Logout>
        </nav>:
        <nav>
          <NavLink href={RoutePaths.login}>{labels["login"]}</NavLink>
        </nav>
    }
  </>
}

const css = `
navlinks {
  margin: 0 2rem;
}`