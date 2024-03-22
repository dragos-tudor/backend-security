import { AccessDenied } from "../errors/access-denied.jsx"
import { loadHome } from "../home/loading.jsx"
import { Login } from "../login/login.jsx"
import { RoutePaths } from "./paths.js"
const { Route } = await import("/scripts/routing.js")

export const Routes = () =>
  <>
    <Route path={RoutePaths.root} child={<></>}></Route>
    <Route path={RoutePaths.home} load={loadHome}></Route>
    <Route path={RoutePaths.login} child={<Login></Login>}></Route>
    <Route path={RoutePaths.accessdenied} child={<AccessDenied></AccessDenied>}></Route>
  </>
