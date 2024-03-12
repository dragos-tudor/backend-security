import { getRouter } from "../router/getting.js";
import { apiFetch } from "../fetch/fetching.js"

export const createAppProps = (settings, document) => Object.freeze({
  ["api-fetch"]: apiFetch(settings.apiUrl, fetch, getRouter(document))
})