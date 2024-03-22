
export const logResponseError = (error, logger = console.error) =>
  logger(error.type, error.message, error.response?.status)