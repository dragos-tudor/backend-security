
export const logError = (error, logger = console.error) =>
  (logger(error.response?.status, error.type, error.message), error)