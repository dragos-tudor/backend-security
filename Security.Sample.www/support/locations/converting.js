import { getLocationUrl } from "./getting.js"

export const toUrl = (location) => new URL(getLocationUrl(location))