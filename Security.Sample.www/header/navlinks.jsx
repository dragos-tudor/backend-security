import { NavLink } from "../deps.js"
import { Logout } from "../logout/logout.jsx"

export const NavLinks = ({user}) =>
  user?
    <nav>
      <NavLink href="/home">Home</NavLink>
      <Logout></Logout>
    </nav>:
    <nav>
      <NavLink href="/login">Login</NavLink>
    </nav>