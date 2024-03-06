import { createAppProps } from "./app/app.props.js"
import { App } from "./app/app.jsx"
const { render }  = await import("/scripts/rendering.js")

const settings = {
  apiUrl: "https://localhost:5000",
  expBackoff: {
    "intervals": [3, 9, 27, 81, 273]
  }
}
render(App(createAppProps(document, settings)), document.body)