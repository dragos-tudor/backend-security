import { getLocationUrl } from "../support/locations/getting.js";

export const existsLocationRedirect = (location) => getLocationUrl(location).includes("redirect")