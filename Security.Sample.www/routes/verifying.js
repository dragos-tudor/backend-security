import { hasLocationPath } from "../support/locations/verifying.js"
import { RoutePaths } from "./paths.js"

export const isHomePath = (location) => hasLocationPath(location, RoutePaths.home)

export const isLoginPath = (location) => hasLocationPath(location, RoutePaths.login)

export const isRootPath = (location) => hasLocationPath(location, RoutePaths.root)