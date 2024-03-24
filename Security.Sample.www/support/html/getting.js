
export const getHtmlBody = (root = document) => root.body

export const getHtmlElement = (root = document, name) => root.querySelector(name.toLowerCase())