import { toLocationUrl } from "../locations/converting.js"

export const getErrorDescription = (location) =>
  toLocationUrl(location)?.searchParams?.get("description")