import { getHtmlBody, getHtmlDescendant } from "../../frontend-shared/html-elements/getting.js"
import { Error } from "../error/error.jsx"

const defaultTimeout = 3000

export const getErrorElement = (elem) => getHtmlDescendant(getHtmlBody(elem), Error.name)

export const getErrorMessage = (error) => error?.message ?? error

export const getPropsMessage = (props) => props.message

export const getPropsTimeout = (props) => props.timeout ?? defaultTimeout
