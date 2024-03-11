import { resilientApiFetch } from "./fetching.js"

export const createAppProps = (settings, document) => Object.freeze({
  ["api-fetch"]: resilientApiFetch(settings, fetch, document)
})