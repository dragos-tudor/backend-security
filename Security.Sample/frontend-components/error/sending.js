import { getErrorMessage } from "./getting.js"
import { findErrorElement } from "./finding.js"
import { updateError } from "./updating.jsx"

export const sendError = (elem) => error => updateError(findErrorElement(elem), getErrorMessage(error))