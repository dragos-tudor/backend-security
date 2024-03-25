import { App } from "./app/app.jsx"
import { createAppProps } from "./app/creating.js"
import { Error } from "./error/error.jsx"
import { existsErrorMessage } from "./error/verifying.js"
import { updateError } from "./error/updating.js"
import { getLangSearchParam } from "./languages/getting.js"
import { Languages } from "./languages/languages.js"
import { setLabels } from "./languages/labels.js"
import { importLabels, importValidationErrors } from "./languages/importing.js"
import { isEnglishLanguage } from "./languages/verifying.js"
import { validateLang } from "./languages/validating.js"
import { RoutePaths } from "./routes/paths.js"
import { getFetchApi } from "./support/api/fetching.js"
import { createJsxElement } from "./support/jsx/creating.js"
import { getHtmlBody, getHtmlElement } from "./support/html/getting.js"
import { logResponseError } from "./support/responses/logging.js"
import { Settings } from "./settings.js"


const { fetchWithTimeout } = await import("/scripts/fetching.js")
const { render } = await import("/scripts/rendering.js")
const { navigate, Router } = await import("/scripts/routing.js")
const { setValidationErrors } = await import("/scripts/validating.js")

const lang = getLangSearchParam(location) ?? Languages.en
validateLang(lang)
if(!isEnglishLanguage(lang)) {
  setLabels(await importLabels(lang))
  setValidationErrors(await importValidationErrors(lang))
}

const fetchApi = getFetchApi(
  (url, request) => fetchWithTimeout(fetch, Settings.apiUrl + url, request, Settings.apiTimeout),
  (url) => navigate($router, url),
  error =>
    logResponseError(error) &&
    existsErrorMessage(error) &&
    updateError($error, error.message)
)
const appProps = createAppProps(fetchApi, Settings.apiUrl)
const $app = render(createJsxElement(App, appProps), getHtmlBody(document))
const $error = render(createJsxElement(Error, {timeout: Settings.errorTimeout}), getHtmlBody(document))
const $router = getHtmlElement($app, Router.name)

navigate($app, RoutePaths.root)


