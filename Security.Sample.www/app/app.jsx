import { Router, Services } from "../deps.js"
import { Footer } from "../footer/footer.jsx"
import { Header } from "../header/header.jsx"
import { Routes } from "../routes/routes.jsx"
import { User } from "../users/user.jsx"
import { services } from "../services/services.js"



export const App = (props) =>
  <>
    <style css={css}></style>
    <Router>
      <Services fetch-api={props[services.fetchApi]} api-url={props[services.apiUrl]}></Services>
      <User>
        <layout>
          <Header></Header>
          <main>
            <Routes></Routes>
          </main>
          <Footer></Footer>
        </layout>
      </User>
    </Router>
  </>

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

router, user, context, routes, route {
  display: block;
  height: inherit;
}`