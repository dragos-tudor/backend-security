import { useLabels, useFetchApi } from "../../frontend-shared/services/using.js"
import { selectAuthenticated } from "../../frontend-shared/store/selectors.js"
import { dispatchAction, useEffect, useSelector, useState } from "../../scripts/extending.js"
import { concatClaim } from "./concating.js"
import { getUser } from "./getting.js"

export const Home = (props, elem) =>
{
  const fetchApi = useFetchApi(elem, props)
  const labels = useLabels(elem)
  const [user, setUser] = useState(elem, "user", null, [])
  const authenticated = useSelector(elem, "authenticated", selectAuthenticated)

  useEffect(elem, "get-user", async () => {
      const [user] = await getUser(fetchApi, dispatchAction(elem))
      user && setUser(user)
    },
  [authenticated])

  return <>
    <style css={css}></style>
    <section>
      <div class="home-user-detail">
        <label>{labels["userName"] + ": "}</label>
        <span>{user?.userName}</span>
      </div>
      <div class="home-user-detail">
        <label>{labels["schemeName"] + ": "}</label>
        <span>{user?.schemeName}</span>
      </div>
      <div class="home-user-detail">
        <label>{labels["userClaims"] + ": "}</label>
        <span>{user?.userClaims?.reduce(concatClaim, "")}</span>
      </div>
    </section>
    {props.children}
  </>
}

const css = `
home {
  display: block;
  margin: 3rem;
}

.home-user-detail {
  color: var(--info-color)
}`