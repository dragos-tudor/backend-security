import { App } from "./app/app.jsx"
import { createAppProps } from "./app/creating.js"
import { getFetchApi } from "./support/api/fetching.js"
import { Settings } from "./settings.js"
const { fetchWithTimeout } = await import("/scripts/fetching.js")
const { render } = await import("/scripts/rendering.js")
const { navigate } = await import("/scripts/routing.js")

const fethApi = getFetchApi(
  (url, request) => fetchWithTimeout(fetch, Settings.apiUrl + url, request, Settings.apiTimeout),
  (url) => navigate(document.querySelector("router"), url),
  console.error
)
const appParent = document.body
const appProps = createAppProps(fethApi, Settings.apiUrl)
render(React.createElement(App, appProps), appParent)