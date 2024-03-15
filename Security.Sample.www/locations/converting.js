
export const toLocationUrl = (location) =>
  typeof location === "string"? new URL(location): location