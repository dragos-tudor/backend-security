import { getContext } from "../scripts/extending.js"
import { renderNavLinks } from "./rendering.jsx"

export const Header = (_, elem) =>
  <>
    <style css={css} ></style>
    <h1 class="logo">
      <a href="/">security sample</a>
    </h1>
    {/* {renderNavLinks(getContext(elem, "user"))} */}
  </>


const css = `
header {
  padding: 1em;
  background-color: var(--dark-primary-color);
}

header .logo {
  font-family: var(--ff-serif);
  text-transform: uppercase;
}

header .logo a {
  margin: 0;
  padding: 0;
  color: var(--text-color);
}
`