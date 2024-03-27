import { google, facebook, twitter, spinner } from "../images/icons.jsx"
import { getLocationUrl } from "../support/locations/getting.js"
import { getApiUrl, getFetchApi, getLabels, getValidationErrors } from "../support/services/getting.js"
import { updateState, useState } from "../scripts/extending.js"
import { createCredentials } from "./creating.js"
import { signInClick } from "./signingin.js"
import { validateCredentials } from "./validating.js"


export const Login = (props, elem) =>
{
  const apiUrl = getApiUrl(elem)
  const fetchApi = getFetchApi(elem)
  const labels = getLabels(elem)
  const validationErrors = getValidationErrors(elem)

  const currentUrl = getLocationUrl(props.location)
  const returnUrl = encodeURIComponent(currentUrl)

  const [userName, setUserName] = useState(elem, "userName", null, [])
  const [password, setPassword] = useState(elem, "password", null, [])
  const [signing, setSigning] = useState(elem, "signing", false, [])

  const credentials = createCredentials(userName, password)
  const validationResult = validateCredentials(credentials, validationErrors)
  const validCredentials = validationResult.isValid

  return <>
    <style css={css}></style>
    <section class="local-authentication">
      <div>
        <label for="userName">{labels["userName"]}</label>
        <input id="userName" type="text" onchange={updateState(setUserName, elem)} placeholder={labels["userName"]}/>
      </div>
      <div>
        <label for="password">{labels["password"]}</label>
        <input id="password" type="password" onchange={updateState(setPassword, elem)} placeholder={labels["password"]}/>
      </div>
      <div>
        <button class="signing" disabled={signing} onclick={() => validCredentials && signInClick(credentials, fetchApi, setSigning, elem)}>
          <span hidden={!signing}>{spinner}</span>
          <span>{labels["signin"]}</span>
        </button>
      </div>
      <div class="error" hidden={userName == null || !validationResult.userName}>
        <label>{labels["userName"]}</label>
        <span>{validationResult.userName}</span>
      </div>
      <div class="error" hidden={password == null || !validationResult.password}>
        <label>{labels["password"]}</label>
        <span>{validationResult.password}</span>
      </div>
    </section>
    <div class="or">or</div>
    <section class="remote-authentication">
      <a class="auth-provider" href={`${apiUrl}/accounts/challenge-google?returnUrl=${returnUrl}`}>
        {google}
        <span>{labels["signinWithGoogle"]}</span>
      </a>
      <a class="auth-provider" href={`${apiUrl}/accounts/challenge-facebook?returnUrl=${returnUrl}`}>
        {facebook}
        <span>{labels["signinWithFacebook"]}</span>
      </a>
      <a class="auth-provider" href={`${apiUrl}/accounts/challenge-twitter?returnUrl=${returnUrl}`}>
        {twitter}
        <span>{labels["signinWithTwitter"]}</span>
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
  border: 3px solid var(--dark-neutral-color);
}

login .local-authentication .error {
  justify-self: start;
  color: var(--error-color)
}

login .or {
  margin: 1em;
  color: var(--light-neutral-color);
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