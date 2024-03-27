import { ensureLocation } from "./ensuring.js"

export const toUrl = (location) =>
  new URL(ensureLocation(location).href)