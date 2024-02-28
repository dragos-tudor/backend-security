import { getContext } from "../extensions/contexts.js"
import { renderNavLinks } from "./rendering.jsx"

export const Header = (_, elem) =>
  <>
    <style css={css} ></style>
    <h4>Security sample</h4>
    {renderNavLinks(getContext(elem, "user"))}
  </>


const css = `
header {
  display: flex;
  justify-content: space-between;
}

header ul {
  list-style: none;
}`