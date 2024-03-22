
export const isUnauthorizedResponse = (response) =>
  response?.status === 401

export const isForbiddenResponse = (response) =>
  response?.status === 403

export const isRedirectResponse = (response) =>
  response?.status > 300 && response?.status < 400