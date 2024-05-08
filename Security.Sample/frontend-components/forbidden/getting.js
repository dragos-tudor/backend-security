import { getLocationSearchParams } from "../../frontend-shared/locations/getting.js"

export const getErrorDescription = (location) =>
  getLocationSearchParams(location).get("description")