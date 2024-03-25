import { createJsxElement } from "../support/jsx/creating.js"
import { Error } from "./error.jsx"
const { update } = await import("/scripts/rendering.js")

const sanitizeMessage = (message) => message.replaceAll("\"", "")

export const updateError = (elem, message) =>
  update(elem, createJsxElement(Error, {message: sanitizeMessage(message)}))