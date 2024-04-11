
export const createLocation = (pathname = "/", search = "", origin = "http://localhost") => Object.freeze({
  href: origin + pathname + search,
  origin,
  pathname,
  search
})
