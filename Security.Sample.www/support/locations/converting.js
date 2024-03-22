
export const toUrl = (location) =>
  typeof location === "string"? new URL(location): location