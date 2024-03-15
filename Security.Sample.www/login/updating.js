import { update } from "../deps.js"

export const updateState = (setState, elem) => (event) => {
  setState(event.target.value)
  return update(elem)
}