import { toUrl } from "./converting.js"

const getSearchParams = (url) => url.searchParams

export const getEncodedLocationPathName = (location) => encodeURIComponent(getLocationPathName(location))

export const getLocationPathName = (location) => location.pathname

export const getLocationUrl = (location) => location.href

export const getLocationParam = (location, paramName) => getSearchParams(toUrl(location)).get(paramName)

