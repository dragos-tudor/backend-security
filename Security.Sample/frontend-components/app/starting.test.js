import { assertEquals } from "/asserts.ts"
import { spy, assertSpyCalls, assertSpyCallArgs } from "/mock.ts"
import { fetchError, fetchOk, createLocation } from "/www/testing.js"
import { RoutePaths } from "../../frontend-shared/route-paths/route.paths.js"
import { createAuthenticatedAction } from "../../frontend-shared/store/actions.js"
import { startApp } from "./starting.js"


Deno.test("app component", async t =>
{
  await t.step("is authenticated response => start app => dispatch is authenticated action", async () =>
  {
    const dispatchSpy = spy(() => {})
    const [authenticated] = await startApp(() => fetchOk(true), dispatchSpy, _, {})

    assertSpyCalls(dispatchSpy, 1)
    assertSpyCallArgs(dispatchSpy, 0, [createAuthenticatedAction(authenticated)])
    assertEquals(authenticated, true)
  })

  await t.step("is not authenticated response => start app => no dispatch action", async () =>
  {
    const dispatchSpy = spy(() => {})
    const [authenticated] = await startApp(() => fetchOk(false), dispatchSpy, _, {})

    assertSpyCalls(dispatchSpy, 0)
    assertEquals(authenticated, false)
  })

  await t.step("error response => start app => no dispatch action", async () =>
  {
    const dispatchSpy = spy(() => {})
    await startApp(fetchError, dispatchSpy, _, {})

    assertSpyCalls(dispatchSpy, 0)
  })

  await t.step("ok response and login location => start app => navigate to home", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchOk, _, navigateSpy, createLocation("/login"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.home])
  })

  await t.step("ok response and root location => start app => navigate to home", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchOk, _, navigateSpy, createLocation("/"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.home])
  })

  await t.step("ok response and info location => start app => navigate to info", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchOk, _, navigateSpy, createLocation("/info"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.info])
  })

  await t.step("error response and root location => start app => navigate to login with home redirection", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchError, _, navigateSpy, createLocation("/"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.login + "?redirect=%2Fhome"])
  })

  await t.step("error response and info location => start app => navigate to login with info redirection", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchError, _, navigateSpy, createLocation("/info"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.login + "?redirect=%2Finfo"])
  })

  await t.step("error response without redirection and login location => start app => navigate to login", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchError, _, navigateSpy, createLocation("/login"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.login])
  })

  await t.step("error response with redirection and login location => start app => navigate to login with redirection", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchError, _, navigateSpy, createLocation("/login", "?redirect=%2Fhome"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.login + "?redirect=%2Fhome"])
  })

})

const _ = () => {}
