const { getService } = await import("/scripts/rendering.js")

const logoutApi = (apiFetch) => apiFetch("POST", "/account/signout")

const logoutUser = (apiFetch) => (event) => {
  event.preventDefault();
  return logoutApi(apiFetch);
}

export const Logout = (props, elem) =>
  <a href="/" onclick={logoutUser(getService(elem, "api-fetch"))}>Logout</a>
