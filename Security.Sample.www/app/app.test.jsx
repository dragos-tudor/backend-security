import { assertExists } from "/asserts.ts"
import { waitForAsyncs } from "/testing.js"
import { App } from "./app.jsx"
const { render } = await import("/scripts/rendering.js")

Deno.test("app component", async t => {

  const getNoUser = () => Promise.resolve([, {}])
  const getUser = () => Promise.resolve([{}])

  await t.step("user unauthenticated [user api return nothing] => render user => navigate to login", async () =>
  {
    const elem = render(<App fetch-api={getNoUser}></App>)
    await waitForAsyncs()

    assertExists(elem.querySelector("login"))
    assertExists(!elem.querySelector("home"))
  })

  await t.step("user authenticated [user api return user] => render user => navigate to home", async () =>
  {
    // const elem = render(<App fetch-api={getUser}></App>)
    // await waitForAsyncs()


    // assertExists(elem.querySelector("home"))
    // assertExists(!elem.querySelector("login"))
  })

  // await t.step("user unauthenticated [user api return nothing] => render user => navigate to login", async () =>
  // {
  //   const elem = render(
  //     <Router>
  //       <Services fetch-api={getNoUser}></Services>
  //       <User>
  //         <Route path="/login" child={<login></login>}></Route>
  //         <Route path="/home" child={<home></home>}></Route>
  //       </User>
  //     </Router>)
  //   await waitForAsyncs()

  //   assertExists(elem.querySelector("login"))
  //   assertExists(!elem.querySelector("home"))
  // })

  // await t.step("user authenticated [user api return user] => render user => navigate to home", async () =>
  // {
  //   const elem = render(
  //     <Router>
  //       <Services fetch-api={getUser}></Services>
  //       <User>
  //         <Route path="/login" child={<login></login>}></Route>
  //         <Route path="/home" child={<home></home>}></Route>
  //       </User>
  //     </Router>)
  //   await waitForAsyncs()

  //   assertExists(elem.querySelector("home"))
  //   assertExists(!elem.querySelector("login"))
  // })

  // await t.step("user rendered => update user => no navigation", async () =>
  // {
  //   const elem = render(
  //     <Router>
  //       <Services fetch-api={getNoUser}></Services>
  //       <User>
  //         <Route path="/login" child={<login></login>}></Route>
  //         <Route path="/home" child={<home></home>}></Route>
  //       </User>
  //     </Router>)
  //   await waitForAsyncs()
  //   navigate(elem, "/home")

  //   update(elem.querySelector("user"),
  //     <User>
  //       <Route path="/login" child={<login></login>}></Route>
  //       <Route path="/home" child={<home></home>}></Route>
  //     </User>)
  //   await waitForAsyncs()

  //   assertExists(elem.querySelector("home"))
  //   assertExists(!elem.querySelector("login"))
  // })

})