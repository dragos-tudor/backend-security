const { getService } = await import("/scripts/rendering.js")

export const getApiFetchService = (elem) =>
  getService(elem, "api-fetch")