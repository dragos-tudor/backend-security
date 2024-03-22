import { google, facebook, twitter } from "../images/icons.jsx"
import { encodeLocationUrl } from "../support/locations/encoding.js"
import { getLocationUrl } from "../support/locations/getting.js"
import { updateState, useState } from "../scripts/extending.js"
import { getFetchApiService, getApiUrlService } from "../support/services/getting.js"
import { signInUser } from "./signingin.js"

export const Login = (props, elem) =>
{
  const apiUrl = getApiUrlService(elem)
  const fetchApi = getFetchApiService(elem)
  const currentUrl = getLocationUrl(props.location)
  const returnUrl = encodeLocationUrl(currentUrl)
  const [userName, setUserName] = useState(elem, "userName", "", [])
  const [password, setPassword] = useState(elem, "password", "", [])

  return <>
    <style css={css}></style>
    <section class="local-authentication">
      <div>
        <label for="userName">User name</label>
        <input id="userName" type="text" onchange={updateState(setUserName, elem)} placeholder="user name here"/>
      </div>
      <div>
        <label for="password">Password</label>
        <input id="password" type="password" onchange={updateState(setPassword, elem)} placeholder="password here"/>
      </div>
      <div>
        <button onclick={() => signInUser({userName, password}, fetchApi, elem)}>Signin with credentials</button>
      </div>
    </section>
    <div class="or">or</div>
    <section class="remote-authentication">
      <a class="auth-provider" href={`${apiUrl}/accounts/challenge-google?returnUrl=${returnUrl}`}>
        {google}
        <span>Signin with Google</span>
      </a>
      <a class="auth-provider" href={`${apiUrl}/accounts/challenge-facebook?returnUrl=${returnUrl}"`}>
        {facebook}
        <span>Signin with Facebook</span>
      </a>
      <a class="auth-provider" href={`${apiUrl}/accounts/challenge-twitter?returnUrl=${returnUrl}"`}>
        {twitter}
        <span>Signin with Twitter</span>
      </a>
    </section>
  </>
}

const css = `
login {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  height: 100%;
}

login .local-authentication {
  display: grid;
  justify-items: end;
  row-gap: 1rem;
}

login .local-authentication,
login .remote-authentication {
  padding: 1em;
  border-radius: var(--default-radius);
  border: 3px solid var(--dark-primary-color);
}

login .or {
  margin: 1em;
  color: var(--light-primary-color);
}

login .remote-authentication .auth-provider {
  display: block;
  margin: 0.5em 0;
}

login .remote-authentication .auth-provider span {
  margin-left: var(--default-margin);
}

@media screen and (max-width: 800px) {
  login {
    flex-direction: column;
  }
}`