import { getLocationSearchParam } from "../locations/getting.js"
import { RoutePaths } from "../route-paths/route.paths.js"
import { RedirectParamName } from "./param.name.js"
import { setRedirectParam } from "./setting.js"

export const getRedirectParam = (location) =>
  decodeURIComponent(getLocationSearchParam(location, RedirectParamName))

export const getRedirectedLogin = (location) =>
  RoutePaths.login + "?" + setRedirectParam(location, RoutePaths.home)