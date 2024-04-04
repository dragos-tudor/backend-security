import { getLocationSearchParams } from "../support/locations/getting.js"

export const getErrorDescription = (location) =>
  getLocationSearchParams(location).get("description")