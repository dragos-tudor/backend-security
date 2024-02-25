import { AccessDeniedError } from "../errors/access-denied-error.jsx"
import { Error } from "../errors/error.jsx"
import { Footer } from "../footer/footer.jsx"
import { Header } from "../header/header.jsx"
import { Login } from "../login/login.jsx"
const { Services } = await import("../scripts/rendering.js")
const { Route, Router } = await import("../scripts/routing.js")


const loadHome = async () => {
  const { Home } = await import("../home/home.jsx");
  fetch()
  return <Home></Home>;
}

export const App = ({fetchJson, apiBaseUrl}) => {
  return <div class="app">
    <Services api-json={(method, path, request) => fetchJson(method)(apiBaseUrl + path, request)}>
      <Router>
        <Header></Header>
        <Route path="/home" load={loadHome}></Route>
        <Route path="/login" child={<Login></Login>}></Route>
        <Route path="/accessdenied" child={<AccessDeniedError></AccessDeniedError>}></Route>
        <Route path="/error" child={<Error></Error>}></Route>
        <Footer></Footer>
      </Router>
    </Services>
  </div>
}



