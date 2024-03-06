const { getEffects, useEffect: usingEffect } = await import("/scripts/rendering.js")

export const useEffect = (elem, name, func, deps) =>
  usingEffect(getEffects(elem), name, func, deps)