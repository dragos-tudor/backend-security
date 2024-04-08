import { spy, assertSpyCalls, assertSpyCallArgs } from "/mock.ts"
import { fetchError, fetchOk, getLocation } from "/www/testing.js"
import { RoutePaths } from "../../support/route-paths/route.paths.js"
import { startApp } from "./starting.js"
import { createSetUserAction } from "../../store/actions.js";

Deno.test("app component", async t =>
{
  await t.step("ok request => start app => dispatch set user action", async () =>
  {
    const dispatchSpy = spy(() => {})
    const [user] = await startApp(fetchOk, dispatchSpy, _, {})

    assertSpyCalls(dispatchSpy, 1)
    assertSpyCallArgs(dispatchSpy, 0, [createSetUserAction(user)])
  })

  await t.step("error request => start app => no dispatch action", async () =>
  {
    const dispatchSpy = spy(() => {})
    await startApp(fetchError, dispatchSpy, _, {})

    assertSpyCalls(dispatchSpy, 0)
  })

  await t.step("ok login request => start app => navigate to home", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchOk, _, navigateSpy, getLocation("/login"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.home])
  })

  await t.step("ok root request => start app => navigate to home", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchOk, _, navigateSpy, getLocation("/"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.home])
  })

  await t.step("ok info request => start app => navigate to info", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchOk, _, navigateSpy, getLocation("/info"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.info])
  })

  await t.step("error root request => start app => navigate to login with home redirection", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchError, _, navigateSpy, getLocation("/"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.login + "?redirect=%2Fhome"])
  })

  await t.step("error info request => start app => navigate to login with info redirection", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchError, _, navigateSpy, getLocation("/info"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.login + "?redirect=%2Finfo"])
  })

  await t.step("error login request without redirection => start app => navigate to login", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchError, _, navigateSpy, getLocation("/login"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.login])
  })

  await t.step("error login request with redirection => start app => navigate to login with redirection", async () =>
  {
    const navigateSpy = spy(() => {})
    await startApp(fetchError, _, navigateSpy, getLocation("/login", "?redirect=%2Fhome"))

    assertSpyCalls(navigateSpy, 1)
    assertSpyCallArgs(navigateSpy, 0, [RoutePaths.login + "?redirect=%2Fhome"])
  })

})

const _ = () => {}
