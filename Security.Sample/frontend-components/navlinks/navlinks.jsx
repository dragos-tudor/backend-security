import { useLabels } from "../../frontend-shared/services/using.js"
import { RoutePaths } from "../../frontend-shared/route-paths/route.paths.js"
import { selectIsAuthenticated } from "../../frontend-shared/store/selectors.js"
import { useSelector } from "../../scripts/extending.js"
const { NavLink } = await import("/scripts/routing.js")


export const NavLinks = (props, elem) =>
{
  const isAuthenticated = useSelector(elem, "is-authenticated", selectIsAuthenticated)
  const labels = useLabels(elem)

  return <>
    <style css={css}></style>
    {
      isAuthenticated?
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