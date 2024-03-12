const { HttpMethods } = await import("/scripts/fetching.js")
const { navigate } = await import("/scripts/routing.js")

const signinApi = (credentials, apiFetch) =>
  apiFetch("/accounts/signin", credentials, { method: HttpMethods.POST })

export const signinUser = async (credentials, apiFetch, setUser, elem) =>
{
  const [user, failure] = await signinApi(credentials, apiFetch)
  if (failure) return failure

  setUser(user)
  navigate(elem, "/home")

  return user
}