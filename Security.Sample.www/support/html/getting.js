
export const getHtmlBody = (document) => document.body

export const getHtmlChildren = (elem) => Array.from(elem.children)

export const getHtmlDescendant = (elem, name) => elem.querySelector(name.toLowerCase())

export const getHtmlName = (elem) => elem.tagName.toLowerCase()

export const getHtmlParent = (elem) => elem.parentElement