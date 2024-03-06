import { assertExists } from "/asserts.ts"
import { render } from "/scripts/rendering.js"
import { waitForAsyncs } from "/testing.js"
import { App } from "./app.jsx"

Deno.test("app component", async t => {

  const getNoUser = (url) => Promise.resolve(url === "/user" && [, {}])
  const getUser = (url) => Promise.resolve(url === "/user" && [{}])

  await t.step("user unauthenticated [user api return nothing] => render user => navigate to login", async () =>
  {
    const elem = render(<App api-fetch={getNoUser}></App>)
    await waitForAsyncs()

    assertExists(elem.querySelector("login"))
    assertExists(!elem.querySelector("home"))
  })

  await t.step("user authenticated [user api return user] => render user => navigate to home", async () =>
  {
    const elem = render(<App api-fetch={getUser}></App>)
    await waitForAsyncs()

    assertExists(elem.querySelector("home"))
    assertExists(!elem.querySelector("login"))
  })

})