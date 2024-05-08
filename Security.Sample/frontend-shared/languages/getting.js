import { getLocationSearchParam } from "../locations/getting.js"
import { LanguageParamName } from "./param.name.js"

export const getLanguageParam = (location) => getLocationSearchParam(location, LanguageParamName)