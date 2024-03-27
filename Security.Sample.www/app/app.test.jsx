import { assertExists } from "/asserts.ts"
import { waitForAsyncs } from "/testing.js"
import { App } from "./app.jsx"
const { render } = await import("/scripts/rendering.js")

Deno.test("app component", async t => {

  const getNoUser = () => Promise.resolve([, {}])
  const getUser = () => Promise.resolve([{}])

  await t.step("user unauthenticated [user api return nothing] => render user => navigate to login", async () =>
  {
    const elem = render(<App fetch-api={getNoUser} labels={{}}></App>)
    await waitForAsyncs()

    assertExists(elem.querySelector("login"))
    assertExists(!elem.querySelector("home"))
  })

  await t.step("user authenticated [user api return user] => render user => navigate to home", async () =>
  {
    const elem = render(<App fetch-api={getUser} labels={{}}></App>)
    await waitForAsyncs()

    assertExists(elem.querySelector("home"))
    assertExists(!elem.querySelector("login"))
  })


})