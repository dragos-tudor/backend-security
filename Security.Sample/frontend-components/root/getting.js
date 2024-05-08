import { getHtmlDescendant} from "../../frontend-shared/html-elements/getting.js"
import { Error } from "../error/error.jsx"
const { Router } = await import("/scripts/routing.js")

export const getErrorElement = (elem) => getHtmlDescendant(elem, Error.name)

export const getRouterElement = (elem) => getHtmlDescendant(elem, Router.name)