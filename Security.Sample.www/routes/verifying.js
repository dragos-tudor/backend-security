import { isLocationUrl } from "../support/locations/verifying.js"
import { RoutePaths } from "./paths.js"

export const isHomePath = (location) => isLocationUrl(location, RoutePaths.home)

export const isLoginPath = (location) => isLocationUrl(location, RoutePaths.login)

export const isRootPath = (location) => isLocationUrl(location, RoutePaths.root)