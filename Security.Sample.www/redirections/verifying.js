import { getLocationUrl } from "../support/locations/getting.js"
import { RedirectParamName } from "./param.name.js"

export const hasLocationRedirect = (location) => getLocationUrl(location).includes(RedirectParamName)