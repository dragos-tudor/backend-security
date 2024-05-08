import { isUnauthorizedResponse } from "../../frontend-shared/responses/verifying.js"

export const IsUnauthorizedError = (error) => isUnauthorizedResponse(error?.response)