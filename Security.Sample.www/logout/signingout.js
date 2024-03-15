


export const signoutUser = (fetchApi) => () =>
  fetchApi("/accounts/signout", null, { method: "POST" })