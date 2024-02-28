import { Logout } from "../logout/logout.jsx"
const { NavLink } = await import("../scripts/routing.js")

export const renderNavLinks = (user) =>
  user?
    <nav>
      <NavLink href="/home">Home</NavLink>
      <NavLink href="/contact">Contact</NavLink>
      <Logout></Logout>
    </nav>:
    <nav>
      <NavLink href="/login">Login</NavLink>
    </nav>