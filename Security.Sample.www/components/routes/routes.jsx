import { Forbidden } from "../forbidden/forbidden.jsx"
import { loadHome } from "../home/loading.jsx"
import { Login } from "../login/login.jsx"
import { Info } from "../info/info.jsx"
import { RoutePaths } from "../../support/route-paths/route.paths.js"
const { Route } = await import("/scripts/routing.js")


export const Routes = () =>
  <>
    <Route path={RoutePaths.home} load={loadHome}></Route>
    <Route path={RoutePaths.login} child={<Login></Login>} index></Route>
    <Route path={RoutePaths.info} child={<Info></Info>}></Route>
    <Route path={RoutePaths.forbidden} child={<Forbidden></Forbidden>}></Route>
  </>
