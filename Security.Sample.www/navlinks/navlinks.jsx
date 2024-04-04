import { useLabels } from "../support/services/using.js"
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
          <NavLink class="navlinks-link" href={RoutePaths.home}>{labels["home"]}</NavLink>
          <NavLink class="navlinks-link" href={RoutePaths.info}>{labels["info"]}</NavLink>
        </nav>:
        <nav>
          <NavLink class="navlinks-link" href={RoutePaths.login}>{labels["login"]}</NavLink>
        </nav>
    }
  </>
}

const css = `
.navlinks-link {
  margin-left: 1rem
}`