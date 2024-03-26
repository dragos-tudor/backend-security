import { Error } from "./error.jsx"
import { sanitizeMessage } from "./sanitizing.js"
const { update } = await import("/scripts/rendering.js")

export const updateError = (elem, message) =>
  update(elem, <Error message={sanitizeMessage(message)}></Error>)