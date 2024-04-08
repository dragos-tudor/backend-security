import { getFetchApi } from "./api/fetching.js"
import { getHtmlDescendant, getHtmlParent } from "./support/html/getting.js"
import { logError } from "./support/errors/logging.js"
import { resolveLocation } from "./support/locations/resolving.js"
import { getLanguageParam } from "./support/languages/getting.js"
import { Languages } from "./support/languages/languages.js"
import { resolveLabels } from "./support/labels/resolving.js"
import { createServices } from "./services/creating.js"
import { resolveValidationErrors } from "./support/validations/resolving.js"
import { Error } from "./components/error/error.jsx"
import { updateError } from "./components/error/updating.jsx"
import { createAppState } from "./store/states.js"
import { createAppReducer } from "./store/reducers.js"
const { fetchWithTimeout } = await import("/scripts/fetching.js")
const { navigate, Router } = await import("/scripts/routing.js")
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

  --neutral--light-color: rgb(168, 166, 166);
  --neutral-color: rgb(49, 46, 46);
  --neutral-dark-color: rgb(41, 39, 39);

  --accent-light-color: rgb(250, 221, 169);
  --accent-color: rgb(255, 192, 74);
  --accent-dark-color: rgb(255, 153, 0);

  --label-color: rgb(140, 231, 140);
  --info-color: rgb(168, 166, 166);
  --warning-color: rgb(255, 253, 111);
  --error-color: rgb(252, 89, 89);

  --font-size: 2rem;
  --transition-interval: 0.5s;
}

@media (prefers-color-scheme: light) {
  :root {
    --neutral--light-color: rgb(255, 235, 235);
    --neutral-color: rgb(207, 207, 207);
    --neutral-dark-color: rgb(231, 228, 228);

    --accent-light-color: rgb(252, 105, 60);
    --accent-color: rgb(247, 119, 80);
    --accent-dark-color: rgb(241, 73, 22);

    --label-color: rgb(1, 160, 36);
    --info-color: rgb(131, 131, 131);
    --warning-color: rgb(255, 253, 111);
    --error-color: rgb(252, 89, 89);
  }
}

*, *:after, *::before {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}



html {
  font-size: 62.5%;
  font-family: var(--ff-sans-serif);
}

body {
  font-size: var(--font-size);
  background-color: var(--neutral-color);
  color: var(--info-color);
}


a {
  text-decoration: none;
  outline: none;
  cursor: pointer;
  transition: color var(--transition-interval) ease-in-out;
  border: 0;
  color: var(--accent-color);
}

a:hover, a:focus {
  color: var(--accent-dark-color);
}

a * {
  vertical-align: middle;
}





input {
  padding: 1rem;
  font-size: var(--font-size);
  outline: none;
  transition: border-color var(--transition-interval) ease-in-out;
  border: thin solid var(--neutral-dark-color);
  background-color: var(--neutral-dark-color);
  color: var(--info-color);
}

input:hover, input:focus {
  border-color: var(--info-color);
}





button {
  padding: 1rem;
  font-size: var(--font-size);
  outline: 0;
  cursor: pointer;
  transition: color var(--transition-interval) ease-in-out, border-color var(--transition-interval);
  border: thin solid var(--accent-color);
  color: var(--accent-color);
  background-color: var(--neutral-dark-color);
}

button:hover, button:focus {
  color: var(--accent-dark-color);
  border-color: var(--accent-dark-color);
}

button:disabled {
  cursor: default;
  color: var(--info-color);
  border-color: var(--neutral--light-color);
}




label {
  cursor: pointer;
  opacity: 75%;
  color: var(--label-color);
}




svg {
  height: 1em;
}`