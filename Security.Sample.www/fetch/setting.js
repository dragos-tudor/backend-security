
export const setFetchRedirect = (request, redirect) =>
  request? Object.assign(request, {redirect}): {redirect}