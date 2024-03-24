import { hideHtmlElement } from "../support/html/hiding.js"
import { showHtmlElement } from "../support/html/showing.js"

export const toggleError = (elem, message, timeout) => () =>
  message &&
  showHtmlElement(elem) &&
  setTimeout(() =>
    hideHtmlElement(elem), timeout)