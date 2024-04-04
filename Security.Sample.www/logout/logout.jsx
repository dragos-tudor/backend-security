import { useFetchApi, useLabels } from "../support/services/using.js"
import { signOutUser } from "./signingout.js"

export const Logout = (_, elem) =>
{
  const fetchApi = useFetchApi(elem)
  const labels = useLabels(elem)

  return (
    <>
      <a onclick={() => signOutUser(fetchApi, elem)}>
        {labels["signout"]}
      </a>
    </>)
}