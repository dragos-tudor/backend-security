import { google, facebook, twitter, spinner } from "../../images/icons.jsx"
import { getLocationUrl } from "../../support/locations/getting.js"
import { resolveLocation } from "../../support/locations/resolving.js"
import { useApiUrl, useFetchApi, useLabels, useValidationErrors } from "../../services/using.js"
import { useState } from "../../scripts/extending.js"
import { createCredentials } from "./creating.js"
import { signInAccount } from "./signingin.js"
import { validateCredentials } from "./validating.js"
import { getHtmlButton } from "./getting.js"
const { navigate } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")

export const Login = (props, elem) =>
{
  const apiUrl = useApiUrl(elem)
  const fetchApi = useFetchApi(elem)
  const labels = useLabels(elem)
  const validationErrors = useValidationErrors(elem)

  const location = resolveLocation(props.location)
  const currentUrl = getLocationUrl(location)
  const returnUrl = encodeURIComponent(currentUrl)

  const [userName, setUserName] = useState(elem, "userName", null, [])
  const [password, setPassword] = useState(elem, "password", null, [])
  const [signing, setSigning] = useState(elem, "signing", false, [])

  const credentials = createCredentials(userName, password)
  const validationResult = validateCredentials(credentials, validationErrors)
  const validCredentials = validationResult.isValid
  const userNameError = userName == null || !validationResult.userName
  const passwordError = password == null || !validationResult.password

  return <>
    <style css={css}></style>
    <section class="local-authentication">
      <label for="userName">{labels["userName"]}</label>
      <input id="userName" type="text" onchange={({target}) => setUserName(target.value)} placeholder={labels["userName"]}/>
      <label for="password">{labels["password"]}</label>
      <input id="password" type="password" onchange={({target}) => setPassword(target.value)} onblur={() => getHtmlButton(elem).focus()} placeholder={labels["password"]}/>
      <button class="signing"
        disabled={!validCredentials || signing}
        onclick={async () => {
          setSigning(true);
          await signInAccount(credentials, location, fetchApi, (action) => dispatchAction(elem, action), (url) => navigate(elem, url));
          setSigning(false);
        }}>
        <span hidden={!signing}>{spinner}</span>
        <span>{labels["signin"]}</span>
      </button>
      <label hidden={userNameError}>{labels["userName"]}</label>
      <span hidden={userNameError} class="error">{validationResult.userName}</span>
      <label hidden={passwordError}>{labels["password"]}</label>
      <span hidden={passwordError} class="error">{validationResult.password}</span>
    </section>
    <div class="or">or</div>
    <section class="remote-authentication">
      <a class="auth-provider" href={`${apiUrl}/accounts/challenge-google?returnUrl=${returnUrl}`}>
        {google}
        <span class="auth-provider-label">{labels["signinWithGoogle"]}</span>
      </a>
      <a class="auth-provider" href={`${apiUrl}/accounts/challenge-facebook?returnUrl=${returnUrl}`}>
        {facebook}
        <span class="auth-provider-label">{labels["signinWithFacebook"]}</span>
      </a>
      <a class="auth-provider" href={`${apiUrl}/accounts/challenge-twitter?returnUrl=${returnUrl}`}>
        {twitter}
        <span class="auth-provider-label">{labels["signinWithTwitter"]}</span>
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
  gap: 2rem;
  height: 100%;
}

@media (max-width: 40rem) {
  login {
    flex-direction: column;
  }
}


.local-authentication,
.remote-authentication {
  padding: 1em;
  border: thick solid var(--neutral-dark-color);
}

.local-authentication {
  display: grid;
  grid-template-columns: auto 1fr;
  justify-items: end;
  align-items: center;
  column-gap: 1rem;
  row-gap: 1rem;
}

.local-authentication .signing {
  grid-column: 1 / span 2;
}

.local-authentication .error {
  color: var(--error-color)
}

.or {
  color: var(--neutral--light-color);
}

.remote-authentication {
  display: grid;
  row-gap: 1rem;
}

.remote-authentication .auth-provider .auth-provider-label {
  margin-left: 0.5rem;
}`