
const signoutApi = (apiFetch) => apiFetch("POST", "/account/signout")

export const signoutUser = (apiFetch) => (event) => {
  event.preventDefault();
  return signoutApi(apiFetch);
}