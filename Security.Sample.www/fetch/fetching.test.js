import { assertEquals } from "/asserts.ts"
import { spy, assertSpyCalls, assertSpyCallArgs } from "/mock.ts"
import { createErrorJsonResponse, createJsonResponse } from "/www/testing.js"
import { RoutePaths } from "../support/route-paths/route.paths.js"
import { getFetchApi } from "./fetching.js"

Deno.test("fetch api", async t =>
{

  await t.step("ok request => fetch api => json response", async () =>
  {
    const fetch = getFetchApi(() => Promise.resolve(createJsonResponse({val: 1})), _, _)
    const [result] = await fetch("", {})

    assertEquals(result, {val: 1})
  })

  await t.step("ok request => fetch api => no navigation", async () =>
  {
    const navigationSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.resolve(createJsonResponse()), navigationSpy, _)
    await fetch("", {})

    assertSpyCalls(navigationSpy, 0)
  })

  await t.step("unauthenticated home request => fetch api => navigation to login with home redirection", async () =>
  {
    const navigationSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.resolve(createErrorJsonResponse(401)), navigationSpy, _, {pathname: "/home"})
    await fetch("", {})

    assertSpyCalls(navigationSpy, 1)
    assertSpyCallArgs(navigationSpy, 0, [RoutePaths.login + "?redirect=%2Fhome"])
  })

  await t.step("unauthenticated root request => fetch api => navigation to login with home redirection", async () =>
  {
    const navigationSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.resolve(createErrorJsonResponse(401)), navigationSpy, _, {pathname: "/"})
    await fetch("", {})

    assertSpyCalls(navigationSpy, 1)
    assertSpyCallArgs(navigationSpy, 0, [RoutePaths.login + "?redirect=%2Fhome"])
  })

  await t.step("unauthenticated login request => fetch api => no navigation", async () =>
  {
    const navigationSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.resolve(createErrorJsonResponse(401)), navigationSpy, _, {pathname: "/login"})
    await fetch("", {})

    assertSpyCalls(navigationSpy, 0)
  })

  await t.step("forbidden request => fetch api => navigation to forbidden", async () =>
  {
    const navigationSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.resolve(createErrorJsonResponse(403)), navigationSpy, _)
    await fetch("", {})

    assertSpyCalls(navigationSpy, 1)
    assertSpyCallArgs(navigationSpy, 0, [RoutePaths.forbidden])
  })

  await t.step("bad request => fetch api => response error", async () =>
  {
    const fetch = getFetchApi(() => Promise.resolve(createErrorJsonResponse(400)), _, _)
    const [, error] = await fetch("", {})

    assertEquals(error.type, "HttpError")
  })

  await t.step("http error request => fetch api => handle error", async () =>
  {
    const handleSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.resolve(createErrorJsonResponse(400)), _, handleSpy)
    const [, error] = await fetch("", {})

    assertSpyCalls(handleSpy, 1)
    assertSpyCallArgs(handleSpy, 0, [error])
  })

  await t.step("network error request => fetch api => handle error", async () =>
  {
    const handleSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.reject("some error"), _, handleSpy)
    const [, error] = await fetch("", {})

    assertSpyCalls(handleSpy, 1)
    assertSpyCallArgs(handleSpy, 0, [error])
  })

  await t.step("http error request => fetch api => no navigation", async () =>
  {
    const navigationSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.resolve(createErrorJsonResponse(400)), navigationSpy, _)
    await fetch("", {})

    assertSpyCalls(navigationSpy, 0)
  })

  await t.step("network error request => fetch api => no navigation", async () =>
  {
    const navigationSpy = spy(() => {})
    const fetch = getFetchApi(() => Promise.reject("some error"), navigationSpy, _)
    await fetch("", {})

    assertSpyCalls(navigationSpy, 0)
  })

})

const _ = () => {}