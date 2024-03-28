import { Header } from "../header/header.jsx"
import { Footer } from "../footer/footer.jsx"
import { Routes } from "../routes/routes.jsx"
import { Error } from "../error/error.jsx"
import { resolveLocation } from "../support/locations/resolving.js"
import { createAppState } from "../support/store/states.js"
import { createAppReducer } from "../support/store/reducers.js"
import { useEffect } from "../scripts/extending.js"
import { startApp } from "./starting.js"
import { getFetchApiProp } from "./getting.js";
const { Services } = await import("/scripts/rendering.js")
const { Router } = await import("/scripts/routing.js")
const { Store } = await import("/scripts/states.js")


export const App = (props, elem) =>
{
  const appState = createAppState()
  const appReducer = createAppReducer()
  const fetchApi = getFetchApiProp(props)
  const location = resolveLocation(props.location)

  useEffect(elem, "start-app", () => startApp(fetchApi, location, elem), [])
  return <>
    <style css={css}></style>
    <Router>
      <Services {...props.services}></Services>
      <Store state={appState} reducer={appReducer}></Store>
      <layout>
        <Header></Header>
        <main>
          <Routes></Routes>
        </main>
        <Footer></Footer>
      </layout>
    </Router>
    <Error timeout={props.errorTimeout}></Error>
  </>
}

const css = `
app {
  display: block;
  height: 100vh;
}

layout {
  display: grid;
  grid-template-rows: min-content 1fr min-content;
  height: inherit;
}

main {
  height: 100%;
}

router, routes, route {
  display: block;
  height: inherit;
}`