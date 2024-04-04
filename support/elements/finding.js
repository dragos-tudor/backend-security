

export const findAscendantElement = (elem, func) => {
  if(func(elem)) return elem
  if(!getParentElement(elem)) return undefined
  return findAscendantElement(getParentElement(elem), func)
}