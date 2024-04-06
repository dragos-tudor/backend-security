import { Header } from "../header/header.jsx"
import { Footer } from "../footer/footer.jsx"
import { Routes } from "../routes/routes.jsx"
import { usePostEffect, useState } from "../scripts/extending.js"
import { resolveLocation } from "../support/locations/resolving.js"
import { useFetchApi } from "../support/services/using.js"
import { Spinner } from "../spinner/spinner.jsx"
import { startApp } from "./starting.js"
const { navigate, Router } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")

export const App = (props, elem) =>
{
  const fetchApi = props.fetch ?? useFetchApi(elem)
  const location = resolveLocation(props?.location)
  const [starting, setStarting] = useState(elem, "starting", true, [])

  usePostEffect(elem, "start-app", async () => {
      await startApp(fetchApi, (action) => dispatchAction(elem, action), (url) => navigate(elem, url), location)
      setStarting(false)
    },
  [])

  return <>
    <style css={css}></style>
    <Router no-skip>
      <Header></Header>
      <main>
        <Spinner spinning={starting} class="app-spinner">
          <Routes></Routes>
        </Spinner>
      </main>
      <Footer></Footer>
    </Router>
  </>
}

const css = `
app {
  height: 100vh;
}

router {
  display: grid;
  grid-template-rows: auto 1fr auto;
  height: inherit;
}

main {
  height: 100%;
}

routes, route {
  display: block;
  height: inherit;
}

.app-spinner {
  background-size: 6rem;
}`