import { usePostEffect } from "../scripts/extending.js";
import { setHtmlBackgroundImage } from "../support/html/setting.js"
import { setSpinnerChildrenVisibility } from "./setting.js"

const DefaultImage = 'url("/images/spinner.svg")'
const NoneImage = "none"

// suspense components toggle children display [non-preserving layout]
// spinner component toggle children visibility [preserving loyout]
export const Spinner = (props, elem) =>
{
  const image = props.image ?? DefaultImage
  const spinning = props.spinning ?? false

  usePostEffect(elem, "set-visibility", () =>
    setSpinnerChildrenVisibility(elem, !spinning), [spinning])

  spinning?
    setHtmlBackgroundImage(elem, image):
    setHtmlBackgroundImage(elem, NoneImage)

  return <>
    <style css={css}></style>
    {...props.children}
  </>
}


const css = `
spinner {
  display: block;
  background-image: none;
  background-position: center;
  background-size: 3rem;
  background-repeat: no-repeat;
  height: inherit;
}`