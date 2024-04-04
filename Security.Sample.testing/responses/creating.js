import { getJsonHeaders, getOkState } from "./getting.js"

export const createResponseInit = (body, contentType, status) => ({
  headers: getJsonHeaders(body, contentType),
  ok: getOkState(status),
  status
})