const { navigate } = await import("../scripts/routing.js")

export const navigateUser = (elem, user) =>
  user? navigate(elem, "/home"): navigate(elem, "/login")