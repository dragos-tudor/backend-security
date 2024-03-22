import { toUrl } from "../support/locations/converting.js"

export const getErrorDescription = (location) =>
  toUrl(location)?.searchParams?.get("description")