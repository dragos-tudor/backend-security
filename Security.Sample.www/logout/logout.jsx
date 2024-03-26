import { getFetchApi, getLabels } from "../support/services/getting.js"
import { signOutUser } from "./signingout.js"

export const Logout = (_, elem) =>
{
  const fetchApi = getFetchApi(elem)
  const labels = getLabels(elem)
  return (
    <>
      <style css={css}></style>
      <button class="signout" onclick={() => signOutUser(fetchApi, elem)}>
        {labels["signout"]}
      </button>
    </>)
}

const css = `
logout .signout {
  font-size: 1.8rem;
  border: 1px solid transparent;
}`