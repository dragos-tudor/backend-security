import { NavLink } from "../scripts/routing.js"
import { Logout } from "../logout/logout.jsx"

export const Header = ({user}) =>
  <div class="header">
    <style css={css} ></style>
    <h4>Security sample</h4>
    <nav>{
      user?
        <ul>
          <li><NavLink href="/home">Home</NavLink></li>
          <li><NavLink href="/account">Account</NavLink></li>
          <li><Logout></Logout></li>
        </ul>:
        <ul>
          <li><NavLink href="/account/login">Login</NavLink></li>
        </ul>
      }
    </nav>
  </div>


const css = `
.header {
  display: flex;
  justify-content: space-between;
}

.header ul {
  list-style: none;
}`