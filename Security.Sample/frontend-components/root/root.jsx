import { getFetchApi } from "../../frontend-shared/fetch/fetching.js"
import { resolveLocation } from "../../frontend-shared/locations/resolving.js"
import { getLanguageParam } from "../../frontend-shared/languages/getting.js"
import { Languages } from "../../frontend-shared/languages/languages.js"
import { resolveLabels } from "../../frontend-shared/labels/resolving.js"
import { createServices } from "../../frontend-shared/services/creating.js"
import { resolveValidationErrors } from "../../frontend-shared/validations/resolving.js"
import { createAccountState, createUserState } from "../../frontend-shared/store/states.js"
import { createAccountReducer, createUserReducer } from "../../frontend-shared/store/reducers.js"
import { Application } from "../app/app.jsx"
import { Error } from "../error/error.jsx"
import { sendError } from "../error/sending.js"
import { navigate } from "../../scripts/extending.js"
const { fetchWithTimeout } = await import("/scripts/fetching.js")
const { Services } = await import("/scripts/rendering.js")
const { Store } = await import("/scripts/states.js")

const language = getLanguageParam(location) ?? Languages.en
const labels = await resolveLabels(language)
const validationErrors = await resolveValidationErrors(language)

export const Root = (props, elem) =>
{
  const {apiUrl, apiTimeout, location} = props
  const fetchApi = getFetchApi(
    (url, request) => fetchWithTimeout(fetch, apiUrl + url, request, apiTimeout),
    navigate(elem),
    sendError(elem),
    resolveLocation(location)
  )
  const services = createServices(apiUrl, fetchApi, labels, language, validationErrors)

  return (
    <>
      <style css={css}></style>
      <Store state={createAccountState({authenticated: false})} reducer={createAccountReducer()}></Store>
      <Store state={createUserState()} reducer={createUserReducer()}></Store>
      <Services {...services}></Services>
      <Application></Application>
      <Error></Error>
    </>
  )
}

const css = `
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

svg {
  height: 1em;
}`