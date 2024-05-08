
export const isAppElement = (elem) => elem.tagName === "APP"

export const isAuthenticationSuccedded = (isAuthenticated, error) => !error && isAuthenticated

