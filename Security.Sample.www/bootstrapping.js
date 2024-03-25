import { App } from "./app/app.jsx"
import { createAppProps } from "./app/creating.js"
import { Error } from "./error/error.jsx"
import { existsErrorMessage } from "./error/verifying.js"
import { updateError } from "./error/updating.js"
import { getLangSearchParam } from "./languages/getting.js"
import { Languages } from "./languages/languages.js"
import { setLanguage } from "./languages/setting.js"
import { RoutePaths } from "./routes/paths.js"
import { getFetchApi } from "./support/api/fetching.js"
import { createJsxElement } from "./support/jsx/creating.js"
import { getHtmlBody, getHtmlElement } from "./support/html/getting.js"
import { logResponseError } from "./support/responses/logging.js"
import { Settings } from "./settings.js"

const { fetchWithTimeout } = await import("/scripts/fetching.js")
const { render } = await import("/scripts/rendering.js")
const { navigate, Router } = await import("/scripts/routing.js")

const lang = getLangSearchParam(location)
await setLanguage(lang ?? Languages.en)

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


