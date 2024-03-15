
export const getAbortSignal = (timeout) =>
{
  const abortController = new AbortController()
  const timeoutId = setTimeout(() => abortController.abort("timeout"), timeout)
  return { signal: abortController.signal, timeoutId }
}

export const getResponseLocation = (response) =>
  response.headers.get("location")