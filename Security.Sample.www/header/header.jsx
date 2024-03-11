import { getContext } from "../scripts/extending.js"
import { NavLinks } from "./navlinks.jsx"

export const Header = (_, elem) =>
  <>
    <style css={css} ></style>
    <h1 class="logo">
      <a href="/">security sample</a>
    </h1>
    <NavLinks user={getContext(elem, "user")}></NavLinks>
  </>


const css = `
header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: var(--dark-primary-color);
}

header .logo {
  margin: 1rem;
  font-family: var(--ff-serif);
  text-transform: uppercase;
}

header .logo a {
  color: var(--text-color);
}

header navlinks {
  margin: 1rem 2rem;
}

header navlink a {
  font-size: 1.3em;
}

header navlink a::after {
  content: "";
  display: block;
  width: 0.5em;
  transition: border-bottom-color var(--default-transition);
  border-bottom: 1px solid transparent;
}

header navlink a:hover::after {
  border-bottom-color: var(--dark-secondary-color);
}`