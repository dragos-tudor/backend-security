import { getHtmlBody } from "../../frontend-shared/html-elements/getting.js"
import { findHtmlDescendant } from "../../frontend-shared/html-elements/finding.js"

export const findErrorElement = (elem) => findHtmlDescendant(getHtmlBody(elem), "ERROR")