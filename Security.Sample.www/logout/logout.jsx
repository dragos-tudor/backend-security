import { getFetchApi } from "../services/getting.js"
import { signoutUser } from "./signingout.js"

export const Logout = (_, elem) =>
{
  const fetchApi = getFetchApi(elem)
  return (
    <>
      <style css={css}></style>
      <button class="signout" onclick={signoutUser(fetchApi, elem)}>
        Signout
      </button>
    </>)
}

const css = `
logout .signout {
  font-size: 1.8rem;
  border: 1px solid transparent;
}`