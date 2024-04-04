
export const getLocationSearchParam = (location, paramName) => getLocationSearchParams(location).get(paramName)

export const getLocationPathName = (location) => location.pathname

export const getLocationPathNameAndSearch = (location) => location.pathname + (location.search ?? "")

export const getLocationSearchParams = (location) => new URLSearchParams(location.search)

export const getLocationUrl = (location) => location.href
