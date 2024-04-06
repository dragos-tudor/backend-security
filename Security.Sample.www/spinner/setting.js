import { getHtmlChildren, getHtmlName } from "../support/html/getting.js"
import { setHtmlVisibility } from "../support/html/setting.js";

export const setSpinnerChildrenVisibility = (elem, visible) =>
  getHtmlChildren(elem)
    .filter(child => getHtmlName(child) !== "style")
    .map(child => (setHtmlVisibility(child, visible), elem))