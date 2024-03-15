
export const setFetchRedirect = (request, redirect) =>
  Object.assign(request, {redirect})

export const setFetchSignal = (request, signal) =>
  Object.assign(request, {signal})