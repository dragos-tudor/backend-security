import { NavLink } from "../deps.js"
import { Logout } from "../logout/logout.jsx"

export const NavLinks = ({user}) =>
  <>
    <style css={css}></style>
    {
      user?
        <nav>
          <NavLink href="/home">Home</NavLink>
          <Logout></Logout>
        </nav>:
        <nav>
          <NavLink href="/login">Login</NavLink>
        </nav>
    }
  </>

const css = `
navlinks {
  margin: 1rem 2rem;
}

navlinks navlink {
  padding-right: 0.7rem;
  font-size: 1.8rem;
}`