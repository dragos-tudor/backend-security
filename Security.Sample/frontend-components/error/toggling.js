import { hideHtmlElement } from "../../frontend-shared/html-elements/hiding.js"
import { showHtmlElement } from "../../frontend-shared/html-elements/showing.js"

export const toggleError = (elem, timeout) =>
  showHtmlElement(elem) &&
  setTimeout(() => hideHtmlElement(elem), timeout)
