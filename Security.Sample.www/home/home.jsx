import { getContext } from "../deps.js"
import { concatClaim } from "./concating.js"

export const Home = (props, elem) =>
{
  const user = getContext(elem, "user")
  return <>
    <style css={css}></style>
    <div class="user-detail">
      <label>User name:</label>
      <span>{user?.userName}</span>
    </div>
    <div class="user-detail">
      <label>Scheme name:</label>
      <span>{user?.schemeName}</span>
    </div>
    <div class="user-detail">
      <label>User claims:</label>
      <span>{user?.userClaims?.reduce(concatClaim, "")}</span>
    </div>
    {props.children}
  </>
}

const css = `
home {
  display: block;
  margin: 3rem;
}

home .user-detail {
  color: var(--text-color)
}`