const { HttpMethods } = await import("/scripts/fetching.js")

export const signOutAccoutApi = (fetchApi)  => fetchApi("/accounts/signout", { method: HttpMethods.POST })