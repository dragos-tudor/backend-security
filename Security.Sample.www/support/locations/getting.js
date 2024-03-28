import { toUrl } from "./converting.js"
import { resolveLocation } from "./resolving.js"

export const getLocationUrl = (location) =>
  resolveLocation(location)?.href

export const getLocationParam = (location, paramName) =>
  toUrl(resolveLocation(location)).searchParams.get(paramName)