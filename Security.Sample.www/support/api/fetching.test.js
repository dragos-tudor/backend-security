import { assertEquals } from "/asserts.ts"
import { spy, assertSpyCalls, assertSpyCallArg } from "/mock.ts"
import { getFetchApi } from "./fetching.js"
import { RoutePaths } from "../../routes/paths.js";

Deno.test("fetch api", async t => {

  await t.step("ok request => fetch api => json response", async () =>
  {
    const fetch = getFetchApi(() => Promise.resolve(getJsonResponse({val: 1})), () => {})
    const [result] = await fetch("", {})

    assertEquals(result, {val: 1})
  })

  await t.step("ok request => fetch api => no navigation", async () =>
  {
    const navigationSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.resolve(getJsonResponse()), navigationSpy)
    await fetch("", {})

    assertSpyCalls(navigationSpy, 0)
  })

  await t.step("unauthenticated request => fetch api => navigation to login", async () =>
  {
    const navigationSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.resolve(getJsonResponse(null, "", 401)), navigationSpy)
    await fetch("", {})

    assertSpyCalls(navigationSpy, 1)
    assertSpyCallArg(navigationSpy, 0, RoutePaths.login)
  })

  await t.step("forbidden request => fetch api => navigation to access denied", async () =>
  {
    const navigationSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.resolve(getJsonResponse(null, "", 403)), navigationSpy)
    await fetch("", {})

    assertSpyCalls(navigationSpy, 1)
    assertSpyCallArg(navigationSpy, 0, RoutePaths.forbidden)
  })

  await t.step("bad request => fetch api => response error", async () =>
  {
    const fetch = getFetchApi(() => Promise.resolve(getJsonResponse(null, "application/json", 400)), () => {})
    const [, error] = await fetch("", {})

    assertEquals(error.type, "HttpError")
  })

  await t.step("non-http error request => fetch api => no navigation", async () =>
  {
    const navigationSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.reject("some error"), navigationSpy)
    await fetch("", {})

    assertSpyCalls(navigationSpy, 0)
  })

})

const getJsonResponse = (body, contentType = "application/json", status = 200) =>
  new Response(body? JSON.stringify(body): null, {
    headers: new Headers({
      "content-length": body? body.length: 0,
      "content-type": contentType
    }),
    ok: status < 400? true: false,
    status
  })