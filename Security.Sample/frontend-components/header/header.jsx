import { Language } from "../language/language.jsx"
import { NavLinks } from "../navlinks/navlinks.jsx"
import { Logout } from "../logout/logout.jsx"
import { selectAuthenticated } from "../../frontend-shared/store/selectors.js"
import { useSelector } from "../../scripts/extending.js"

export const Header = (props, elem) =>
{
  const authenticated = useSelector(elem, "authenticated", selectAuthenticated)

  return (<>
    <style css={css} ></style>
    <h2>
      <a href="/" class="header-logo">security sample</a>
    </h2>
    <Language class="header-language"></Language>
    <NavLinks class="header-navlinks"></NavLinks>
    {authenticated && <Logout></Logout>}
  </>)
}


const css = `
header {
  display: flex;
  align-items: end;
  gap: 2rem;
  padding: 2rem;
  background-color: var(--neutral-dark-color);
}

.header-logo {
  font-family: var(--ff-serif);
  color: var(--label-color);
  text-transform: uppercase;
}

.header-language {
  flex: 1 1;
  text-align: right;
}`