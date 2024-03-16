import { getContext } from "../deps.js"
import { NavLinks } from "../navlinks/navlinks.jsx"

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
  color: var(--label-color);
}`