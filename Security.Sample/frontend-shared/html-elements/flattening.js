import { getHtmlChildren } from "./getting.js";

export const flatHtmlChildren = (elems) => elems.flatMap(elem => getHtmlChildren(elem))