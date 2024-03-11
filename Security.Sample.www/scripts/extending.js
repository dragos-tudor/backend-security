const { getContexts, getEffects, getStates, useState: usingState , useEffect: usingEffect, useContext } = await import("/scripts/rendering.js")

export const getContext = (elem, name, initialValue) =>
  useContext(getContexts(elem), name, initialValue, elem)[0]

export const useEffect = (elem, name, func, deps) =>
  usingEffect(getEffects(elem), name, func, deps)

export const useState = (elem, name, value, deps) =>
  usingState(getStates(elem), name, value, deps)