import { getLocationPathName } from "../locations/getting.js"
import { isRootPath } from "../route-paths/verifying.js"
import { RedirectParamName } from "./param.name.js"

const resolveRedirectParamPath = (location, fallback) =>
  isRootPath(location)? fallback: getLocationPathName(location)

export const setRedirectParam = (location, fallback) =>
  RedirectParamName + "=" + encodeURIComponent(resolveRedirectParamPath(location, fallback))