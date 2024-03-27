import { toUrl } from "./converting.js"

export const getLocation = (location) =>
  (location ?? globalThis.location)

export const getLocationUrl = (location) =>
  getLocation(location)?.href

export const getLocationParam = (location, paramName) =>
  toUrl(getLocation(location)).searchParams.get(paramName)