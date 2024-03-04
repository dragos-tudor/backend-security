import { App } from "./app/app.jsx"
const { fetchJson } = await import("./scripts/fetching.js")
const { render }  = await import("./scripts/rendering.js")
const { navigate }  = await import("./scripts/routing.js")

const apiFetch = async (url, data, request) => {
  const [result, error] = await fetchJson(fetch, "https://localhost:5000" + url, data, request)
  if (result?.status === 401) return navigate(document.querySelector("router"), "/login")
  if (result?.status === 403) return navigate(document.querySelector("router"), "/accessdenied")
  if (error) return console.error(result) // TODO: handle error
  return result
}

render(App({["api-fetch"]: apiFetch}), document.body)