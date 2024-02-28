const { getStates, useState: usingState } = await import("../scripts/rendering.js")

export const useState = (elem, name, value, deps) =>
  usingState(getStates(elem), name, value, deps)