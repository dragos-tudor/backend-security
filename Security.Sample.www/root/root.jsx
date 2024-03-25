import { spinner } from "../images/icons.jsx"

export const Root = () =>
  <>
    <style css={css}></style>
    {spinner}
  </>

const css = `
root {
  display: flex;
  height: 100%;
  justify-content: center;
}

root svg {
  height: 5rem;
  align-self: center;
}`