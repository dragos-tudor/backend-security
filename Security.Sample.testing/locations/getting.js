
export const getLocation = (pathname = "/", search = "", origin = "http://localhost") => Object.freeze({
  href: origin + pathname + search,
  origin,
  pathname,
  search
})
