import { resolveLocation } from "../../frontend-shared/locations/resolving.js"
import { useFetchApi } from "../../frontend-shared/services/using.js"
import { spinner } from "../../images/icons.jsx"
import { dispatchAction, navigate, usePostEffect, useState } from "../../scripts/extending.js"
import { Header } from "../header/header.jsx"
import { Footer } from "../footer/footer.jsx"
import { Routes } from "../routes/routes.jsx"
import { startApp } from "./starting.js"
const { Router } = await import("/scripts/routing.js")

export const Application = (props, elem) =>
{
  const fetchApi = useFetchApi(elem, props)
  const location = resolveLocation(props.location)
  const [starting, setStarting] = useState(elem, "starting", true, [])

  usePostEffect(elem, "start-app", async () => {
      await startApp(fetchApi, dispatchAction(elem), navigate(elem), location)
      setStarting(false)
    },
  [])

  return <>
    <style css={css}></style>
    <Router no-skip>
      <Header></Header>
      <main>
        <div hidden={!starting} class="app-spinner">{spinner}</div>
        <Routes></Routes>
      </main>
      <Footer></Footer>
    </Router>
  </>
}

const css = `
application {
  height: 100vh;
}

router {
  display: grid;
  grid-template-rows: auto 1fr auto;
  height: inherit;
}

main {
  height: 100%;
  justify-self: center;
  align-self: center;
}

routes, route, suspense {
  display: block;
  height: inherit;
}

main {
  display: grid;
  justify-items: center;
  align-items: center;
}

.app-spinner svg  {
  height: 5rem;
}`