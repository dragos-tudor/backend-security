import { Route } from "../deps.js"
import { AccessDenied } from "../errors/access-denied.jsx"
import { Login } from "../login/login.jsx"
import { loadHome } from "../home/loading.jsx"


export const routes = Object.freeze({
  root: "/",
  login: "/login",
  home: "/home",
  accessdenied: "/accessdenied"
})

export const Routes = () =>
  <>
    <Route path={routes.root} child={<></>}></Route>
    <Route path={routes.home} load={loadHome}></Route>
    <Route path={routes.login} child={<Login></Login>}></Route>
    <Route path={routes.accessdenied} child={<AccessDenied></AccessDenied>}></Route>
  </>