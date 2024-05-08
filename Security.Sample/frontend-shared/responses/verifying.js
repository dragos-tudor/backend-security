
export const isForbiddenResponse = (response) => response?.status === 403

export const isUnauthorizedResponse = (response) => response?.status === 401