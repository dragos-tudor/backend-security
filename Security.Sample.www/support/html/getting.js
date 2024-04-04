
export const getHtmlBody = (document) => document.body

export const getHtmlDescendant = (element, name) => element.querySelector(name.toLowerCase())

export const getHtmlParent = (element) => element.parentElement