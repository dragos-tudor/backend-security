import { hideHtmlElement } from "../support/html/hiding.js"
import { showHtmlElement } from "../support/html/showing.js"
import { setInitialEffect } from "../scripts/extending.js"

export const toggleError = (elem, timeout) => () => {
  showHtmlElement(elem)
  const timeoutId = setTimeout(() => hideHtmlElement(elem), timeout)
  return setInitialEffect(elem, "toggle-error", () => clearTimeout(timeoutId))
}