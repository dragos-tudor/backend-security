import { toLocationUrl } from "./converting.js"

export const getLocationHref = (location) =>
  toLocationUrl(location)?.href ?? globalThis.location?.href

export const getLocationOrigin = (location) =>
  toLocationUrl(location)?.origin ?? globalThis.location?.origin
