import { getHtmlBody } from "../frontend-shared/html-elements/getting.js"
const rendering = await import("/scripts/rendering.js")
const routing = await import("/scripts/routing.js")
const states = await import("/scripts/states.js")

const { setEffects, setStates, update } = rendering
const { getStoreStates, setSelectors } = states

export const dispatchAction = (elem) =>
  (action) => states.dispatchAction(elem, action)

export const navigate = (elem) =>
  (url) => routing.navigate(getHtmlBody(elem), url)

export const setInitialEffect = (elem, name, func) =>
  rendering.setInitialEffect(setEffects(elem), name, func)

export const useEffect = (elem, name, func, deps) =>
  rendering.useEffect(setEffects(elem), name, func, deps)

export const usePostEffect = (elem, name, func, deps) =>  // run after rendering elements
  rendering.useEffect(setEffects(elem), name, async () => (await Promise.resolve(), func()), deps)

export const useSelector = (elem, name, func) =>
  states.useSelector(setSelectors(elem), name, func, getStoreStates(elem))

export const useState = (elem, name, value, deps) => // convenient useState function
{
  const [state, setState] = rendering.useState(setStates(elem), name, value, deps)
  return [state, (value) => { const result = setState(value); update(elem); return result; }]
}