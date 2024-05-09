import { isUnauthorizedResponse } from "../../frontend-shared/responses/verifying.js"

export const isSigninError = (error) => !!error

export const isUnauthorizedError = (error) => isUnauthorizedResponse(error?.response)