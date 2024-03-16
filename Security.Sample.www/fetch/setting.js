
export const setFetchCredentials = (request, value = "omit") =>
  Object.assign(request, {credentials: value})

export const setFetchRedirect = (request, redirect) =>
  Object.assign(request, {redirect})

export const setFetchSignal = (request, signal) =>
  Object.assign(request, {signal})