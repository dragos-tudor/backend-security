import { flatHtmlChildren } from "./flattening.js"
import { existsHtmlElements, isHtmlElement } from "./verifying.js"

const findHtmlElement = (elems, name) => elems.find(elem => isHtmlElement(elem, name))

export const findHtmlDescendant = (elem, name) => findHtmlsDescendant([elem], name)

export const findHtmlsDescendant = (elems, name) =>
  findHtmlElement(elems, name) ||
  (existsHtmlElements(elems)?
    findHtmlsDescendant(flatHtmlChildren(elems), name):
    undefined)
