
const toLocationUrl = (location) =>
  typeof location === "string"? new URL(location): location

export const getErrorName = (location) =>
  toLocationUrl(location).searchParams?.get("name")

export const getErrorDescription = (location) =>
  toLocationUrl(location).searchParams?.get("description")