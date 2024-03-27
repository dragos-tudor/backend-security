import { useFetchApi, useLabels } from "../support/services/using.js"
import { signOutUser } from "./signingout.js"

export const Logout = (_, elem) =>
{
  const fetchApi = useFetchApi(elem)
  const labels = useLabels(elem)

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