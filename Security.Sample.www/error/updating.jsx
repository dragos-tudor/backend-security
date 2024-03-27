import { Error } from "./error.jsx"
import { sanitizeMessage } from "./sanitizing.js"
import { existsErrorMessage } from "./verifying.js"
const { update } = await import("../scripts/rendering.js")

export const updateError = (elem, message) =>
  existsErrorMessage(message) &&
  update(elem, <Error message={sanitizeMessage(message)}></Error>)