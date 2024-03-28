import { resolveLocation } from "./resolving.js"

export const toUrl = (location) =>
  new URL(resolveLocation(location).href)