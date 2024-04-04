import { getHtmlDescendant } from "../support/html/getting.js"
import { setHtmlBackgroundImage, setHtmlVisibility } from "../support/html/setting.js"
import { Spinner } from "./spinner.jsx"

const SpinnerImage = 'url("/images/spinner.svg")'

export const showSpinner = (elem, image = SpinnerImage) =>
{
  const spinner = getHtmlDescendant(elem, Spinner.name)
  if (!spinner) return

  setHtmlBackgroundImage(spinner, image)
  setHtmlVisibility(spinner.children[1], false)
  return spinner
}
