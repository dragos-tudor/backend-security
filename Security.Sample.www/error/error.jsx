import { useEffect } from "../scripts/extending.js"
import { toggleError } from "./toggling.js"


export const Error = ({message, timeout}, elem) => {
  const showTimeout = timeout ?? 3500
  useEffect(elem, "toggle", toggleError(elem, message, showTimeout))

  return <>
    <style css={css}></style>
    <p>{message}</p>
  </>
}

const css = `
error {
  display: none;
  position: absolute;
  transition: display 1s ease-in-out;
  bottom: 3rem;
  left: 2rem;
  padding: 1rem;
  border: 1px solid var(--error-color);
  color: var(--light-primary-color);
  background-color: var(--primary-color);
}`