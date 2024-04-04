import { assertExists } from "/asserts.ts"
import { waitForAsyncs } from "/testing.js"
import { fetchError, fetchOk, getLocation } from "/www/testing.js"
import { App } from "./app.jsx"
const { render } = await import("/scripts/rendering.js")

Deno.test("app component", async t =>
{
  await t.step("user authenticated => render app => navigate to home", async () =>
  {
    const elem = render(
      <App fetch={fetchOk} location={getLocation()} __ignore={["header", "home"]}></App>)
    await waitForAsyncs()

    assertExists(elem.querySelector("home"))
    assertExists(!elem.querySelector("login"))
  })

  await t.step("user unauthenticated => render app => navigate to login", async () =>
  {
    const elem = render(
      <App fetch={fetchError} location={getLocation()} __ignore={["header", "login"]}></App>)
    await waitForAsyncs()

    assertExists(elem.querySelector("login"))
    assertExists(!elem.querySelector("home"))
  })

})