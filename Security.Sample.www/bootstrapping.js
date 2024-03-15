import { App } from "./app/app.jsx"
import { createAppProps } from "./app/creating.js"
import { getRouter } from "./router/getting.js";
import { render } from "./deps.js"
import { settings } from "./settings.js"

const appParent = document.body
const appProps = createAppProps(settings, getRouter(document))
render(React.createElement(App, appProps), appParent)