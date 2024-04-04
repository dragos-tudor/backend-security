const { getStoreStates, setSelectors, useSelector: _useSelector, dispatchAction: _dispatchAction } = await import("./states.js")
const { setEffects, setStates, update, useEffect: _useEffect, useState: _useState, setInitialEffect: _setInitialEffect } = await import("./rendering.js")

export const setInitialEffect = (elem, name, func) =>
  _setInitialEffect(setEffects(elem), name, func)

export const useEffect = (elem, name, func, deps) =>
  _useEffect(setEffects(elem), name, func, deps)

export const useSelector = (elem, name, func) =>
  _useSelector(setSelectors(elem), name, func, getStoreStates(elem))

export const useState = (elem, name, value, deps) =>
  _useState(setStates(elem), name, value, deps)

export const updateState = (setState, elem) => (event) => {
  setState(event.target.value)
  return update(elem)
}