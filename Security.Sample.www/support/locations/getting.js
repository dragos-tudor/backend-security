import { toUrl } from "./converting.js"

export const getLocationOrigin = (location) =>
  toUrl(location)?.origin ?? globalThis.location?.origin

export const getLocationUrl = (location) =>
  toUrl(location)?.href ?? globalThis.location?.href
