import { getContexts, useContext } from "../scripts/rendering.js"

export const getContext = (elem, name, initialValue) =>
  useContext(getContexts(elem), name, initialValue, elem)[0]