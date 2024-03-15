const { render, update, unrender, Context, Services, Lazy, Suspense, getStates, getEffects, getContexts, getService } = await import("/scripts/rendering.js")
const { Route, Router, NavLink, navigate } = await import("/scripts/routing.js")
const { fetchJson, HttpMethods } = await import("/scripts/fetching.js")
const rendering = await import("/scripts/rendering.js")

const getContext = (elem, name, initialValue) =>
  useContext(elem, name, initialValue)[0]

const setContext = (elem, name, initialValue) =>
  useContext(elem, name, initialValue)[1]

const useContext = (elem, name, initialValue) =>
  rendering.useContext(getContexts(elem), name, initialValue, elem)

const useEffect = (elem, name, func, deps) =>
  rendering.useEffect(getEffects(elem), name, func, deps)

const useState = (elem, name, value, deps) =>
  rendering.useState(getStates(elem), name, value, deps)

export {
  render, update, unrender, Context, Services, Lazy, Suspense, useState, useEffect, getContext, getService, setContext, useContext,
  Route, Router, NavLink, navigate,
  fetchJson, HttpMethods
}