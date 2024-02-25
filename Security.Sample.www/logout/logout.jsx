
const logoutApi = async (userName, fetchApi) => {
  await fetchApi("POST", "/account/signout", {userName})
}

export const Logout = ({userName, fetchApi}) =>
  <a href="/account/login" onclick={async (event) => { event.preventDefault(); await logoutApi(userName, fetchApi); }}>Logout</a>
