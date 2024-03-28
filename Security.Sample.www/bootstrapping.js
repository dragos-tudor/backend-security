import { getFetchApi } from "./support/api/fetching.js"
import { logError } from "./support/errors/logging.js"
import { createJsxElement } from "./support/jsx/creating.js"
import { getHtmlBody, getHtmlElement } from "./support/html/getting.js"
import { getLocationParam } from "./support/locations/getting.js"
import { resolveLocation } from "./support/locations/resolving.js"
import { LanguageParamName } from "./support/languages/param.name.js"
import { Languages } from "./support/languages/languages.js";
import { validateLanguage } from "./support/languages/validating.js"
import { resolveLabels } from "./support/labels/resolving.js"
import { createServices } from "./support/services/creating.js"
import { resolveValidationErrors } from "./support/validations/resolving.js"
import { App } from "./app/app.jsx"
import { Error } from "./error/error.jsx"
import { updateError } from "./error/updating.jsx"
import { RoutePaths } from "./routes/paths.js"
const { render } = await import("/scripts/rendering.js")
const { fetchWithTimeout } = await import("/scripts/fetching.js")
const { changeRoute, navigate, Router } = await import("/scripts/routing.js")
const { apiUrl, apiTimeout,  errorTimeout } = await import("/settings.js")

const location = resolveLocation(globalThis.localation)
const fetchApi = getFetchApi(
  (url, request) => fetchWithTimeout(fetch, apiUrl + url, request, apiTimeout),
  (url) => navigate($router, url),
  error => (logError(error), updateError($error, error.message)),
  location
)
const language = validateLanguage(getLocationParam(location, LanguageParamName) ?? Languages.en)
const labels = await resolveLabels(language)
const validationErrors = await resolveValidationErrors(language)
const services = createServices(apiUrl, fetchApi, language, labels, validationErrors)

const $app = render(createJsxElement(App, {services, errorTimeout}), getHtmlBody(document))
const $error = getHtmlElement($app, Error.name)
const $router = getHtmlElement($app, Router.name)

changeRoute($router, RoutePaths.root)


