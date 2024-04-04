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

  return <>
    <style css={css}></style>
    <section class="local-authentication">
      <div>
        <label for="userName">{labels["userName"]}</label>
        <input id="userName" type="text" onchange={updateState(setUserName, elem)} placeholder={labels["userName"]}/>
      </div>
      <div>
        <label for="password">{labels["password"]}</label>
        <input id="password" type="password" onchange={updateState(setPassword, elem)} onblur={() => getHtmlButton(elem).focus()} placeholder={labels["password"]}/>
      </div>
      <div>
        <Spinner no-skip>
          <button disabled={!validCredentials} onclick={async () =>
              showSpinner(elem) &&
              await signInUser(credentials, location, fetchApi, elem) &&
              hideSpinner(elem)
            }>
            <span>{labels["signin"]}</span>
          </button>
        </Spinner>
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

.local-authentication {
  display: grid;
  justify-items: end;
  row-gap: 1rem;
}

.local-authentication,
.remote-authentication {
  padding: 1em;
  border-radius: var(--default-radius);
  border: 3px solid var(--dark-neutral-color);
}

.local-authentication .error {
  justify-self: start;
  color: var(--error-color)
}

.or {
  margin: 1em;
  color: var(--light-neutral-color);
}

.remote-authentication .auth-provider {
  display: block;
  margin: 0.5em 0;
}

.remote-authentication .auth-provider span {
  margin-left: var(--default-margin);
}

@media (max-width: 800px) {
  login {
    flex-direction: column;
  }
}`