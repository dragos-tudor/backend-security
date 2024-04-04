import { getFetchApi } from "./support/api/fetching.js"
import { getHtmlDescendant, getHtmlParent } from "./support/html/getting.js"
import { logError } from "./support/errors/logging.js"
import { resolveLocation } from "./support/locations/resolving.js"
import { getLanguageParam } from "./support/languages/getting.js"
import { Languages } from "./support/languages/languages.js"
import { resolveLabels } from "./support/labels/resolving.js"
import { createServices } from "./support/services/creating.js"
import { resolveValidationErrors } from "./support/validations/resolving.js"
import { Error } from "./error/error.jsx"
import { updateError } from "./error/updating.jsx"
import { createAppState } from "./support/store/states.js"
import { createAppReducer } from "./support/store/reducers.js"
const { fetchWithTimeout } = await import("/scripts/fetching.js")
const { navigate } = await import("/scripts/routing.js")
const { Services } = await import("/scripts/rendering.js")
const { Store } = await import("/scripts/states.js")
const { apiUrl, apiTimeout,  errorTimeout } = await import("/settings.js")

const language = getLanguageParam(location) ?? Languages.en
const labels = await resolveLabels(language)
const validationErrors = await resolveValidationErrors(language)

export const Index = (props, elem) =>
{
  const appState = createAppState()
  const appReducer = createAppReducer()
  const location = resolveLocation(props.location)
  const fetchApi = getFetchApi(
    (url, request) => fetchWithTimeout(fetch, apiUrl + url, request, apiTimeout),
    (url) => navigate(getHtmlDescendant(getHtmlParent(elem), Router.name), url),
    error => (logError(error), updateError(getHtmlDescendant(elem, Error.name), error.message)),
    location
  )
  const services = createServices(apiUrl, fetchApi, labels, validationErrors, language)

  return (
    <>
      <style css={css}></style>
      <Store state={appState} reducer={appReducer}></Store>
      <Services {...services}></Services>
      <Error timeout={errorTimeout}></Error>
    </>
  )
}

const css = `
:root {
  --ff-serif: Georgia, 'Times New Roman', Times, serif;
  --ff-sans-serif: Arial, Helvetica, sans-serif;

  --light-neutral-color: rgb(168, 166, 166);
  --neutral-color: rgb(49, 46, 46);
  --dark-neutral-color: rgb(41, 39, 39);

  --light-accent-color: rgb(250, 221, 169);
  --accent-color: rgb(255, 192, 74);
  --dark-accent-color: rgb(255, 153, 0);

  --label-color: rgb(140, 231, 140);
  --info-color: rgb(168, 166, 166);
  --warning-color: rgb(255, 253, 111);
  --error-color: rgb(252, 89, 89);

  --default-font-size: 2rem;
  --default-padding: 1rem;
  --default-margin: 0.5rem;
  --default-transition: 0.5s;
}

html {
  font-size: 62.5%;
  font-family: var(--ff-sans-serif);
}

body {
  font-size: var(--default-font-size);
  background-color: var(--neutral-color);
  color: var(--info-color);
}

* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}




a {
  text-decoration: none;
  outline: none;
  cursor: pointer;
  transition: color var(--default-transition) ease-in-out;
  border: 0;
  color: var(--accent-color);
}

a:hover, a:focus {
  color: var(--dark-accent-color);
}

a * {
  vertical-align: middle;
}





input {
  padding: var(--default-padding);
  font-size: var(--default-font-size);
  outline: none;
  transition: border-color var(--default-transition) ease-in-out;
  border: thin solid var(--dark-neutral-color);
  background-color: var(--dark-neutral-color);
  color: var(--light-neutral-color);
}

input:hover, input:focus {
  border-color: var(--info-color);
}





button {
  padding: var(--default-padding);
  font-size: var(--default-font-size);
  outline: 0;
  cursor: pointer;
  transition: color var(--default-transition) ease-in-out, border-color var(--default-transition);
  border: thin solid var(--accent-color);
  color: var(--accent-color);
  background-color: var(--dark-neutral-color);
}

button * {
  vertical-align: middle;
}

button:hover, button:focus {
  color: var(--dark-accent-color);
  border-color: var(--dark-accent-color);
}

button:disabled {
  cursor: default;
  color: var(--light-neutral-color);
  border-color: var(--light-neutral-color);
}




label {
  cursor: pointer;
  opacity: 75%;
  color: var(--label-color);
}




svg {
  height: 1em;
}


.spinner {
  display: block;
  background-image: url("/images/spinner.svg");
  background-position: center;
  background-size: 3rem;
  background-repeat: no-repeat;
  height: inherit;
}`