import { getHtmlDescendant } from "../support/html/getting.js"
import { setHtmlBackgroundImage, setHtmlVisibility } from "../support/html/setting.js"
import { Spinner } from "./spinner.jsx"

export const showSpinner = (elem, image = 'url("/images/spinner.svg")') =>
{
  const spinner = getHtmlDescendant(elem, Spinner.name)
  if (!spinner) return elem

  setHtmlBackgroundImage(spinner, image)
  setHtmlVisibility(spinner.children[1], false)
  return spinner
}
