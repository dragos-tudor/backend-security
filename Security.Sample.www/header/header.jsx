import { Language } from "../language/language.jsx"
import { NavLinks } from "../navlinks/navlinks.jsx"

export const Header = (_, elem) =>
 <>
    <style css={css} ></style>
    <h1>
      <a href="/" class="logo">security sample</a>
    </h1>
    <Language></Language>
    <NavLinks></NavLinks>
  </>


const css = `
header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem;
  background-color: var(--dark-neutral-color);
}

header .logo {
  font-family: var(--ff-serif);
  color: var(--label-color);
  text-transform: uppercase;
}`