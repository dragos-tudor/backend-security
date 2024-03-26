import { App } from "./app/app.jsx"
import { createAppProps } from "./app/creating.js"
import { Error } from "./error/error.jsx"
import { existsErrorMessage } from "./error/verifying.js"
import { updateError } from "./error/updating.jsx"
import { getLanguage } from "./support/languages/getting.js"
import { resolveLabels } from "./support/labels/resolving.js"
import { resolveValidationErrors } from "./support/validations/resolving.js"
import { validateLanguage } from "./support/languages/validating.js"
import { RoutePaths } from "./routes/paths.js"
import { getFetchApi } from "./support/api/fetching.js"
import { createJsxElement } from "./support/jsx/creating.js"
import { getHtmlBody, getHtmlElement } from "./support/html/getting.js"
import { logResponseError } from "./support/responses/logging.js"
import { Settings } from "./settings.js"

const { render } = await import("/scripts/rendering.js")
const { fetchWithTimeout } = await import("/scripts/fetching.js")
const { navigate, Router } = await import("/scripts/routing.js")

const fetchApi = getFetchApi(
  (url, request) => fetchWithTimeout(fetch, Settings.apiUrl + url, request, Settings.apiTimeout),
  (url) => navigate($router, url),
  error =>
    logResponseError(error) &&
    existsErrorMessage(error) &&
    updateError($error, error.message)
)
const lang = validateLanguage(getLanguage(location))
const appProps = createAppProps(Settings, fetchApi, await resolveLabels(lang), await resolveValidationErrors(lang))

const $app = render(createJsxElement(App, appProps), getHtmlBody(document))
const $error = getHtmlElement($app, Error.name)
const $router = getHtmlElement($app, Router.name)

navigate($app, RoutePaths.root)


