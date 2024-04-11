import { useFetchApi, useLabels } from "../../services/using.js"
import { signOutAccount } from "./signingout.js"

export const Logout = (_, elem) =>
{
  const fetchApi = useFetchApi(elem)
  const labels = useLabels(elem)

  return (
    <>
      <a onclick={() => signOutAccount(fetchApi, elem)}>
        {labels["signout"]}
      </a>
    </>)
}