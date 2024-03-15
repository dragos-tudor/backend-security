import { google, facebook, twitter } from "../images/icons.jsx"
import { getLocationOrigin } from "../locations/getting.js"
import { setContext, useState } from "../deps.js"
import { getFetchApi, getApiUrl } from "../services/getting.js"
import { signinUser } from "./signingin.js"
import { updateState } from "./updating.js"


export const Login = (props, elem) =>
{
  const apiUrl = getApiUrl(elem)
  const apiFetch = getFetchApi(elem)
  const appOrigin = getLocationOrigin(props.location)
  const returnUrl = encodeURIComponent(appOrigin)
  const setUser = setContext(elem, "user")

  const [userName, setUserName] = useState(elem, "userName", "", [])
  const [password, setPassword] = useState(elem, "password", "", [])

  return <>
    <style css={css}></style>
    <signin-form>
      <div>
        <label for="userName">User name</label>
        <input id="userName" type="text" onchange={updateState(setUserName, elem)} placeholder="user name here"/>
      </div>
      <div>
        <label for="password">Password</label>
        <input id="password" type="password" onchange={updateState(setPassword, elem)} placeholder="password here"/>
      </div>
      <div>
        <button onclick={() => signinUser({userName, password}, apiFetch, setUser, elem)}>Signin with credentials</button>
      </div>
    </signin-form>
    <or>or</or>
    <external-auth>
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
    </external-auth>
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

signin-form {
  display: grid;
  justify-items: end;
  row-gap: 1rem;
}

signin-form, external-auth {
  padding: 1em;
  border-radius: var(--default-radius);
  border: 3px solid var(--dark-primary-color);
}

or {
  margin: 1em;
  color: var(--light-primary-color);
}

external-auth .auth-provider {
  display: block;
  margin: 0.5em 0;
}

external-auth .auth-provider span {
  margin-left: var(--default-margin);
}

@media screen and (max-width: 800px) {
  login {
    flex-direction: column;
  }
}`