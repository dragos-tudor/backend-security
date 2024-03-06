const { navigate } = await import("/scripts/routing.js")

export const navigateToAccessDenied = (router) =>
  navigate(router, "/accessdenied")

export const navigateToLogin = (router) =>
  navigate(router, "/login")