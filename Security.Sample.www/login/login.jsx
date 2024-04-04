import { google, facebook, twitter } from "../images/icons.jsx"
import { getLocationUrl } from "../support/locations/getting.js"
import { resolveLocation } from "../support/locations/resolving.js"
import { useApiUrl, useFetchApi, useLabels, useValidationErrors } from "../support/services/using.js"
import { updateState, useState } from "../scripts/extending.js"
import { hideSpinner } from "../spinner/hiding.js"
import { showSpinner } from "../spinner/showing.js"
import { Spinner } from "../spinner/spinner.jsx"
import { createCredentials } from "./creating.js"
import { signInUser } from "./signingin.js"
import { validateCredentials } from "./validating.js"
import { getHtmlButton } from "./getting.js";


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

  const credentials = createCredentials(userName, password)
  const validationResult = validateCredentials(credentials, validationErrors)
  const validCredentials = validationResult.isValid
  const userNameVisibility = userName == null || !validationResult.userName
  const passwordVisibility = password == null || !validationResult.password

  return <>
    <style css={css}></style>
    <section class="local-authentication">
      <label for="userName">{labels["userName"]}</label>
      <input id="userName" type="text" onchange={updateState(setUserName, elem)} placeholder={labels["userName"]}/>
      <label for="password">{labels["password"]}</label>
      <input id="password" type="password" onchange={updateState(setPassword, elem)} onblur={() => getHtmlButton(elem).focus()} placeholder={labels["password"]}/>
      <Spinner class="signing-spinner" no-skip>
        <button disabled={!validCredentials} onclick={async () =>
            showSpinner(elem) &&
            await signInUser(credentials, location, fetchApi, elem) &&
            hideSpinner(elem)
          }>
          <span>{labels["signin"]}</span>
        </button>
      </Spinner>
      <label hidden={userNameVisibility}>{labels["userName"]}</label>
      <span hidden={userNameVisibility} class="error">{validationResult.userName}</span>
      <label hidden={passwordVisibility}>{labels["password"]}</label>
      <span hidden={passwordVisibility} class="error">{validationResult.password}</span>
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

.local-authentication,
.remote-authentication {
  padding: 1em;
  border: thick solid var(--dark-neutral-color);
}

.local-authentication {
  display: grid;
  grid-template-columns: auto 1fr;
  justify-items: end;
  align-items: center;
  column-gap: 1rem;
  row-gap: 1rem;
}

.local-authentication .signing-spinner {
  grid-column: 1 / span 2;
}

.local-authentication .error {
  color: var(--error-color)
}

.or {
  color: var(--light-neutral-color);
}

.remote-authentication {
  display: grid;
  row-gap: 1rem;
}

.remote-authentication .auth-provider .auth-provider-label {
  margin-left: var(--default-margin);
}

@media (max-width: 40rem) {
  login {
    flex-direction: column;
  }
}`