import { selectUserAuthenticated } from "../support/store/selecting.js";
import { Logout } from "../logout/logout.jsx"
import { useSelector } from "../scripts/extending.js"
const { NavLink } = await import("/scripts/routing.js")


export const NavLinks = (props, elem) =>
{
  const authenticated = props.authenticated ?? useSelector(elem, "authenticated", selectUserAuthenticated)
  return <>
    <style css={css}></style>
    {
      authenticated?
        <nav>
          <NavLink href="/home">Home</NavLink>
          <Logout></Logout>
        </nav>:
        <nav>
          <NavLink href="/login">Login</NavLink>
        </nav>
    }
  </>
}

const css = `
navlinks {
  margin: 1rem 2rem;
}

navlinks navlink {
  padding-right: 0.7rem;
  font-size: 1.8rem;
}`