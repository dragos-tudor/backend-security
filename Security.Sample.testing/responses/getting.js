import { createResponseInit } from "./creating.js"
import { stringifyBody } from "./stringify.js"

export const getJsonHeaders = (body, contentType) => new Headers({
  "content-length": body? body.length: 0,
  "content-type": contentType
})

export const getOkState = (status) => status < 400? true: false

export const getErrorJsonResponse = (status) => getJsonResponse(null, "", status)

export const getJsonResponse = (body, contentType = "application/json", status = 200) =>
  new Response(stringifyBody(body), createResponseInit(body, contentType, status))

