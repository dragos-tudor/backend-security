const { getContexts, useContext } = await import("/scripts/rendering.js")

export const getContext = (elem, name, initialValue) =>
  useContext(getContexts(elem), name, initialValue, elem)[0]