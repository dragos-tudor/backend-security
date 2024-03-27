import { toUrl } from "./converting.js"
import { ensureLocation } from "./ensuring.js"

export const getLocationUrl = (location) =>
  ensureLocation(location)?.href

export const getLocationParam = (location, paramName) =>
  toUrl(ensureLocation(location)).searchParams.get(paramName)