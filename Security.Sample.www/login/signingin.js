const { HttpMethods } = await import("/scripts/fetching.js")
const { navigate } = await import("/scripts/routing.js")

export const signinUser = async (credentials, apiFetch, elem) => {
  const [_, failure] = await apiFetch("/accounts/signin", credentials, { method: HttpMethods.POST })
  return failure || navigate(elem, "/home")
}