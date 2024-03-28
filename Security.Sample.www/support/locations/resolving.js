
export const resolveLocation = (location) => (location ?? globalThis.location) ?? {href: ""}