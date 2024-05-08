import { assertExists } from "/asserts.ts"
import { waitForAsyncs } from "/testing.js"
import { fetchError, fetchOk, createLocation } from "/www/testing.js"
import { Application } from "./app.jsx"
const { render } = await import("/scripts/rendering.js")

Deno.test("app component", async t =>
{
  await t.step("user authenticated => render app => navigate to home", async () =>
  {
    const elem = render(
      <Application fetch-api={fetchOk} location={createLocation()} __ignore={["header", "home"]}></Application>)
    await waitForAsyncs()

    assertExists(elem.querySelector("home"))
    assertExists(!elem.querySelector("login"))
  })

  await t.step("user unauthenticated => render app => navigate to login", async () =>
  {
    const elem = render(
      <Application fetch-api={fetchError} location={createLocation()} __ignore={["header", "login"]}></Application>)
    await waitForAsyncs()

    assertExists(elem.querySelector("login"))
    assertExists(!elem.querySelector("home"))
  })

})