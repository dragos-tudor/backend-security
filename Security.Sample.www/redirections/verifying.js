import { getLocationUrl } from "../support/locations/getting.js"
import { RedirectParamName } from "./param.name.js"

export const existsLocationRedirect = (location) => getLocationUrl(location).includes(RedirectParamName)