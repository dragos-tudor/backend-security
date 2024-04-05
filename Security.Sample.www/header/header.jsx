import { Language } from "../language/language.jsx"
import { NavLinks } from "../navlinks/navlinks.jsx"
import { Logout } from "../logout/logout.jsx"
import { selectUserAuthenticated } from "../support/store/selectors.js"
const { useSelector } = await import("../scripts/extending.js")

export const Header = (props, elem) =>
{
  const authenticated = useSelector(elem, "authenticated", selectUserAuthenticated)

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