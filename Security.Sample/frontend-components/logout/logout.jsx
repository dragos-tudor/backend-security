import { useFetchApi, useLabels } from "../../frontend-shared/services/using.js"
import { dispatchAction, navigate } from "../../scripts/extending.js"
import { signOutAccount } from "./signingout.js"

export const Logout = (props, elem) =>
{
  const fetchApi = useFetchApi(elem, props)
  const labels = useLabels(elem)

  return (
    <>
      <a onclick={() => signOutAccount(fetchApi, dispatchAction(elem), navigate(elem))}>
        {labels["signout"]}
      </a>
    </>)
}