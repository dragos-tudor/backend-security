import { toUrl } from "./converting.js"

export const getLocationUrl = (location) =>
  toUrl(location ?? globalThis.location)?.href

export const getSearchParam = (location, paramName) =>
  toUrl(location?.href)?.searchParams?.get(paramName)