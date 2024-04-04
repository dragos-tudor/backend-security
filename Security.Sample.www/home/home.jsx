import { useLabels } from "../support/services/using.js"
import { selectUser } from "../support/store/selectors.js"
import { useSelector } from "../scripts/extending.js"
import { concatClaim } from "./concating.js"

export const Home = (props, elem) =>
{
  const user = useSelector(elem, "user", selectUser)
  const labels = useLabels(elem)

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