import { getErrorElement, getErrorMessage } from "./getting.js"
import { updateError } from "./updating.jsx"

export const sendError = (elem) => error => updateError(getErrorElement(elem), getErrorMessage(error))