
export const isAppElement = (elem) => elem.tagName === "APP"

export const isAuthenticationSuccedded = (authenticated, error) => !error && authenticated

