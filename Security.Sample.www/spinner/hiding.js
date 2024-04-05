import { getHtmlChildren, getHtmlDescendant, getHtmlName } from "../support/html/getting.js"
import { setHtmlBackgroundImage, setHtmlVisibility } from "../support/html/setting.js"
import { Spinner } from "./spinner.jsx"

export const hideSpinner = (elem) =>
{
  const spinner = getHtmlDescendant(elem, Spinner.name)
  if (!spinner) return

  setHtmlBackgroundImage(spinner, "none")
  getHtmlChildren(spinner)
    .filter(child => getHtmlName(child) !== "style")
    .forEach(child => setHtmlVisibility(child, true))
  return spinner
}
