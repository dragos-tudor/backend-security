
export const getJsonHeaders = (body, contentType) => new Headers({
  "content-length": body? body.length: 0,
  "content-type": contentType
})

export const getOkState = (status) => status < 400? true: false



