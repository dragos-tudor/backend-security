import { App } from "./app/app.jsx"
import { createAppProps } from "./app/creating.js"
import { settings } from "./settings.js"
const { render }  = await import("/scripts/rendering.js")

render(React.createElement(App, createAppProps(settings, document)), document.body)