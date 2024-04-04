import { Header } from "../header/header.jsx"
import { Footer } from "../footer/footer.jsx"
import { Routes } from "../routes/routes.jsx"
import { usePostEffect } from "../scripts/extending.js"
import { resolveLocation } from "../support/locations/resolving.js"
import { useFetchApi } from "../support/services/using.js"
import { Spinner } from "../spinner/spinner.jsx"
import { hideSpinner } from "../spinner/hiding.js"
import { showSpinner } from "../spinner/showing.js";
import { startApp } from "./starting.js"
const { navigate, Router } = await import("/scripts/routing.js")
const { dispatchAction } = await import("/scripts/states.js")

export const App = (props, elem) =>
{
  const fetchApi = props.fetch ?? useFetchApi(elem)
  const location = resolveLocation(props?.location)

  usePostEffect(elem, "start-app", async () => {
      showSpinner(elem)
      await startApp(fetchApi, (action) => dispatchAction(elem, action), (url) => navigate(elem, url), location)
      hideSpinner(elem)
    },
  [])

  return <>
    <style css={css}></style>
    <Router>
      <Header></Header>
      <main>
        <Spinner class="app-spinner">
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