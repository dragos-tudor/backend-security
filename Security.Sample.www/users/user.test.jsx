import { assertExists } from "/asserts.ts"
import { render, update, Services } from "/scripts/rendering.js"
import { navigate, Router, Route } from "/scripts/routing.js"
import { User } from "./user.jsx"

Deno.test("user component", async t => {

  const getNoUser = (url) => Promise.resolve(url === "/user" && [, {}])
  const getUser = (url) => Promise.resolve(url === "/user" && [{}])

  await t.step("user unauthenticated [user api return nothing] => render user => navigate to login", async () =>
  {
    const elem = render(
      <Router>
        <Services api-fetch={getNoUser}></Services>
        <User>
          <Route path="/login" child={<login></login>}></Route>
          <Route path="/home" child={<home></home>}></Route>
        </User>
      </Router>)
    await Promise.resolve()

    assertExists(elem.querySelector("login"))
    assertExists(!elem.querySelector("home"))
  })

  await t.step("user authenticated [user api return user] => render user => navigate to home", async () =>
  {
    const elem = render(
      <Router>
        <Services api-fetch={getUser}></Services>
        <User>
          <Route path="/login" child={<login></login>}></Route>
          <Route path="/home" child={<home></home>}></Route>
        </User>
      </Router>)
    await Promise.resolve()

    assertExists(elem.querySelector("home"))
    assertExists(!elem.querySelector("login"))
  })

  await t.step("user rendered => update user => no navigation", async () =>
  {
    const elem = render(
      <Router>
        <Services api-fetch={getNoUser}></Services>
        <User>
          <Route path="/login" child={<login></login>}></Route>
          <Route path="/home" child={<home></home>}></Route>
        </User>
      </Router>)
    await Promise.resolve()
    navigate(elem, "/home")

    update(elem.querySelector("user"),
      <User>
        <Route path="/login" child={<login></login>}></Route>
        <Route path="/home" child={<home></home>}></Route>
      </User>)
    await Promise.resolve()

    assertExists(elem.querySelector("home"))
    assertExists(!elem.querySelector("login"))
  })

})