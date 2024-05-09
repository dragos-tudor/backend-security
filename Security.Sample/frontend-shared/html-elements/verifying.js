import { getHtmlName } from "./getting.js"

export const existsHtmlElements = (elems) => elems.length !== 0

export const isHtmlElement = (elem, name) => getHtmlName(elem) === name