import { getLocationPathName } from "./getting.js"

export const isLocationUrl = (location, url) => getLocationPathName(location) === url