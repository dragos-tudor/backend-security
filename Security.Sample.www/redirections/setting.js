import { RoutePaths } from "../routes/paths.js"
import { isRootPath, isLoginPath } from "../routes/verifying.js";
import { getEncodedLocationPathName } from "../support/locations/getting.js"
import { RedirectParamName } from "./param.name.js"

export const setLoginRedirectUrl = (location) =>
  isLoginPath(location) || isRootPath(location)?
    RoutePaths.login:
    RoutePaths.login + `?${RedirectParamName}=${getEncodedLocationPathName(location)}`