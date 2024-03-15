
export const isResponseUnauthorized = (response) =>
  response?.statusCode === 401

export const isResponseForbidden = (response) =>
  response?.statusCode === 403

export const isResponseRedirect = (response) =>
  response?.statusCode > 300 && response?.statusCode < 400