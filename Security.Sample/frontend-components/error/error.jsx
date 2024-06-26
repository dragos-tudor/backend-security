import { setInitialEffect, useEffect, useState } from "../../scripts/extending.js"
import { getPropsMessage, getPropsTimeout } from "./getting.js"
import { toggleError } from "./toggling.js"

export const Error = (props, elem) =>
{
  const [timeout] = useState(elem, "timeout", getPropsTimeout(props), [])
  const message = getPropsMessage(props)

  if(message)
    useEffect(elem, "toggle-error", () => {
      const timeoutId = toggleError(elem, timeout)
      setInitialEffect(elem, "toggle-error", () => clearTimeout(timeoutId))
    })

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
  bottom: 5rem;
  left: 2rem;
  padding: 1rem;
  border: thin solid var(--error-color);
  color: var(--info-color);
  background-color: var(--neutral-light-color);
}`