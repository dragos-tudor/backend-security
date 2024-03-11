const { update } = await import("/scripts/rendering.js")

export const updateState = (setState, elem) => (event) => {
  setState(event.target.value)
  return update(elem)
}