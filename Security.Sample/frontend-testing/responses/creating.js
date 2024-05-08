import { getJsonHeaders, getOkState } from "./getting.js"
import { stringifyBody } from "./stringify.js"

const createResponseInit = (body, contentType, status) => ({
  headers: getJsonHeaders(body, contentType),
  ok: getOkState(status),
  status
})

export const createErrorJsonResponse = (status) =>
  createJsonResponse(null, "", status)

export const createJsonResponse = (body, contentType = "application/json", status = 200) => new Response(
  stringifyBody(body),
  createResponseInit(body, contentType, status)
)