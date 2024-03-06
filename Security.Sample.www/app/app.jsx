import { AccessDeniedError } from "../errors/access-denied-error.jsx"
import { Error } from "../errors/error.jsx"
import { Footer } from "../footer/footer.jsx"
import { Header } from "../header/header.jsx"
import { loadHome } from "../home/loading.jsx";
import { Login } from "../login/login.jsx"
import { User } from "../users/user.jsx"
const { Services } = await import("/scripts/rendering.js")
const { Route, Router } = await import("/scripts/routing.js")


export const App = (props) =>
  <Router>
    <Services api-fetch={props["api-fetch"]}></Services>
    <User>
      <Header></Header>
      <Route path="/" child={<></>}></Route>
      <Route path="/home" load={loadHome}></Route>
      <Route path="/login" child={<Login></Login>}></Route>
      <Route path="/accessdenied" child={<AccessDeniedError></AccessDeniedError>}></Route>
      <Route path="/error" child={<Error></Error>}></Route>
      <Footer></Footer>
    </User>
  </Router>




