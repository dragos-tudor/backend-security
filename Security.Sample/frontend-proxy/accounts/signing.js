const { HttpMethods } = await import("/scripts/fetching.js")

export const signInAccountApi = (credentials, fetchApi) =>
  fetchApi("/accounts/signin", { method: HttpMethods.POST, body: credentials })