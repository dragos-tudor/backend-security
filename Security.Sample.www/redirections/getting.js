import { RoutePaths } from "../routes/paths.js"
import { getLocationParam } from "../support/locations/getting.js"
import { RedirectParamName } from "./param.name.js"
import { existsLocationRedirect } from "./verifying.js"

export const getRedirectOrHomeUrl = (location) =>
  existsLocationRedirect(location)? getLocationParam(location, RedirectParamName): RoutePaths.home