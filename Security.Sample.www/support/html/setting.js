
const getHtmlVisibility = (visible) => visible? "visible": "hidden"

export const setHtmlBackgroundImage = (elem, image) => (elem.style.backgroundImage = image)

export const setHtmlClassName = (elem, className) => (elem.className = className)

export const setHtmlVisibility = (elem, visible) => (elem.style.visibility = getHtmlVisibility(visible))


