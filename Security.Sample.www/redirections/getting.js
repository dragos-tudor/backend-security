import { getLocationParam } from "../support/locations/getting.js"
import { RedirectParamName } from "./param.name.js"

export const getLocationRedirect = (location) => getLocationParam(location, RedirectParamName)