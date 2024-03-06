import { apiFetch, resilientFetch } from "../api/fetching.js"
import { navigateToAccessDenied, navigateToLogin } from "./navigating.js"
import { getAppRouter } from "./getting.js"

export const createAppProps = (document, settings) => Object.freeze({
  ["api-fetch"]:
    resilientFetch(
      settings.expBackoff?.intervals,
      apiFetch(
        settings.apiUrl,
        fetch,
        () => navigateToAccessDenied(getAppRouter(document)),
        () => navigateToLogin(getAppRouter(document))
      )
    )
})