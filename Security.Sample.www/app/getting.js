import { findAscendantElement } from "../support/elements/finding.js"
import { ServiceNames } from "../support/services/names.js"
import { isAppElement } from "./verifying.js"

export const getApp = (elem) => findAscendantElement(elem, isAppElement)

export const getApiUrl = (props) => props[ServiceNames.apiUrl]

export const getFetchApi = (props) => props[ServiceNames.fetchApi]

export const getRouter = (elem) => elem.querySelector("router")
