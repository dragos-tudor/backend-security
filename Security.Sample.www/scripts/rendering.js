// deno-lint-ignore-file no-control-regex
const createHtmlElement = (document, tagName)=>document.createElement(tagName);
const createHtmlElementNS = (document, ns, tagName)=>document.createElementNS(ns, tagName);
const getHtmlName = (elem)=>elem.tagName?.toLowerCase() || "text";
const getHtmlOwnerDocument = (elem)=>elem?.ownerDocument;
const getHtmlParentElement = (elem)=>elem?.parentElement;
const iterateHtmlChildren = (elem, func)=>{
    for(let index = 0; index < elem.children.length; index++)func(elem.children[index]);
};
const findHtmlAscendant = (elem, func)=>{
    if (func(elem)) return elem;
    if (!getHtmlParentElement(elem)) return undefined;
    return findHtmlAscendant(getHtmlParentElement(elem), func);
};
const findHtmlDescendants = (elem, func, elems = [])=>{
    if (func(elem)) elems.push(elem);
    iterateHtmlChildren(elem, (child)=>findHtmlDescendants(child, func, elems));
    return elems;
};
const HtmlMimeType = "text/html";
const parseHtml = (html)=>new DOMParser().parseFromString(html, HtmlMimeType).documentElement;
const isEventHandler = (name)=>name.startsWith("on");
const JavaScriptProtocolRegex = /^[\u0000-\u001F ]*j[\r\n\t]*a[\r\n\t]*v[\r\n\t]*a[\r\n\t]*s[\r\n\t]*c[\r\n\t]*r[\r\n\t]*i[\r\n\t]*p[\r\n\t]*t[\r\n\t]*\:/i;
const UnsafeTagNames = Object.freeze([
    "SCRIPT",
    "IFRAME"
]);
const UnsafePropNames = Object.freeze([
    "style",
    "css",
    "innerHTML",
    "outerHTML"
]);
const UrlPropNames = Object.freeze([
    "action",
    "background",
    "dynsrv",
    "href",
    "lowsrc",
    "src"
]);
const isFunctionPropValue = (props, propName)=>typeof props[propName] === "function";
const isSafePropNameForTag = (propName, tagName)=>tagName === "style" && propName === "css";
const isSafeEventHandler = (props, propName)=>isEventHandler(propName) && isFunctionPropValue(props, propName);
const isSafePropName = (tagName, propName)=>isSafePropNameForTag(propName, tagName) || !UnsafePropNames.includes(propName);
const isSafeTagName = (tagName)=>!UnsafeTagNames.includes(tagName.toUpperCase());
const isSafeUrl = (props, propName)=>UrlPropNames.includes(propName) ? !JavaScriptProtocolRegex.test(props[propName] || "") : true;
const isHtmlElement = (elem)=>elem.nodeType === 1;
const validateHtmlElement = (elem)=>isHtmlElement(elem) ? "" : "Element type should be HTML Element.";
const validateHtmlTagName = (name)=>isSafeTagName(name) ? "" : "Unsafe html tag " + name;
const createCustomEvent = (eventName, detail)=>new CustomEvent(eventName, {
        bubbles: true,
        cancelable: true,
        detail
    });
const dispatchEvent = (elem, eventName, detail)=>elem.dispatchEvent(createCustomEvent(eventName, detail));
const AriaPropMappings = Object.freeze({
    "aria-autocomplete": "ariaAutoComplete",
    "aria-colcount": "ariaColCount",
    "aria-colindex": "ariaColIndex",
    "aria-colindextext": "ariaColIndexText",
    "aria-haspopup": "ariaHasPopUp",
    "aria-keyshortcuts": "ariaKeyShortcuts",
    "aria-multiselectable": "ariaMultiSelectable",
    "aria-posinset": "ariaPosInSet",
    "aria-readonly": "ariaReadOnly",
    "aria-roledescription": "ariaRoleDescription",
    "aria-rowcount": "ariaRowCount",
    "aria-rowindex": "ariaRowIndex",
    "aria-rowspan": "ariaRowSpan",
    "aria-setsize": "ariaSetSize",
    "aria-valuemax": "ariaValueMax",
    "aria-valuemin": "ariaValueMin",
    "aria-valuenow": "ariaValueNow",
    "aria-valuetext": "ariaValueText"
});
const SpecialPropMappings = Object.freeze({
    class: "className",
    for: "htmlFor",
    readonly: "readOnly",
    tabindex: "tabIndex",
    css: "innerHTML",
    html: "innerHTML"
});
const ReservedPropNames = Object.freeze([
    "children"
]);
const isAriaPropName = (propName)=>propName.startsWith("aria-");
const isSpecialPropName = (propName)=>SpecialPropMappings[propName];
const isReservedPropName = (propName)=>ReservedPropNames.includes(propName);
const isValidPropName = (elem, props, propName)=>!isReservedPropName(propName) && !isEventHandler(propName) && isSafePropName(getHtmlName(elem), propName) && isSafeUrl(props, propName);
const getPropNames = (elem)=>Object.getOwnPropertyNames(elem);
const getValidPropNames = (elem, props)=>getPropNames(props).filter((propName)=>isValidPropName(elem, props, propName));
const getEventName = (handlerName)=>handlerName.replace("on", "");
const getValidEventHandlerNames = (props)=>getPropNames(props).filter((propName)=>isSafeEventHandler(props, propName));
const storeEventHandler = (elem, handlerName, handler)=>elem[handlerName] = handler;
const unstoreEventHandler = (elem, handlerName)=>delete elem[handlerName];
const unsetEventHandler = (elem, handlerName)=>{
    elem.removeEventListener(getEventName(handlerName), elem[handlerName]);
    unstoreEventHandler(elem, handlerName);
    return handlerName;
};
const unsetEventHandlers = (elem, props)=>getValidEventHandlerNames(props).map((handlerName)=>unsetEventHandler(elem, handlerName));
const setEventHandler = (elem, handlerName, handler)=>{
    unsetEventHandler(elem, handlerName);
    storeEventHandler(elem, handlerName, handler);
    return handlerName;
};
const setEventHandlers = (elem, props)=>getValidEventHandlerNames(props).map((handlerName)=>setEventHandler(elem, handlerName, props[handlerName]));
const appendHtmlNode = (node, parent)=>parent.appendChild(node);
const getHtmlChildNode = (node, index)=>node.childNodes[index];
const getHtmlChildNodes = (node)=>Array.from(node.childNodes);
const getHtmlParentNode = (node)=>node.parentNode;
const insertHtmlNode = (node, oldNode)=>getHtmlParentNode(oldNode).insertBefore(node, oldNode) && node;
const removeHtmlNode = (node)=>getHtmlParentNode(node).removeChild(node);
const replaceHtmlNode = (node, oldNode)=>getHtmlParentNode(oldNode).replaceChild(node, oldNode);
const DOMLibraryUrl = "https://esm.sh/linkedom@0.14.26";
const registerDOMParser = async (url = DOMLibraryUrl, global = globalThis)=>{
    const dom = await import(url);
    global.DOMParser = global.DOMParser || dom.DOMParser;
    global.CustomEvent = dom.CustomEvent;
    return global.DOMParser;
};
const isFunctionAttrValue = (attrValue)=>typeof attrValue === "function";
const isNamespaceAttrName = (attrName)=>attrName === "xmlns";
const setAttribute = (elem, attrName, attrValue)=>isFunctionAttrValue(attrValue) || isNamespaceAttrName(attrName) || elem.setAttributeNS?.(null, attrName, attrValue);
const toCamelCaseName = (attrName)=>`aria${attrName[5].toUpperCase()}${attrName.substring(6)}`;
const mapPropName = (propName)=>isSpecialPropName(propName) && SpecialPropMappings[propName] || isAriaPropName(propName) && (AriaPropMappings[propName] || toCamelCaseName(propName)) || propName;
const EncodingCharsRegex = /[^\w. ]/gi;
const getHtmlEntity = (__char)=>`&#${__char.charCodeAt(0)};`;
const encodeHtml = (string)=>string.replace(EncodingCharsRegex, getHtmlEntity);
const TogglePropNames = Object.freeze([
    "checked",
    "disabled",
    "hidden",
    "readOnly",
    "selected"
]);
const isDangerouslyHtmlPropName = (propName)=>propName === "html";
const isEmptyPropValue = (propValue)=>propValue == undefined || propValue === "";
const isTogglePropName = (propName)=>TogglePropNames.includes(propName);
const getTogglePropValue = (propValue)=>isEmptyPropValue(propValue) || propValue;
const resolvePropValue = (props, propName)=>isDangerouslyHtmlPropName(propName) && encodeHtml(props[propName]) || isTogglePropName(propName, props[propName]) && getTogglePropValue(props[propName]) || props[propName];
const isSVGPropValue = (elem, propName)=>elem[propName]?.constructor?.name.startsWith("SVG");
const isHtmlPropName = (elem, propName)=>propName in elem || propName.startsWith("__");
const isWritableHtmlProp = (elem, propName)=>{
    const descriptor = Object.getOwnPropertyDescriptor(elem, propName);
    if (descriptor && "writable" in descriptor) return descriptor.writable;
    if (descriptor && "set" in descriptor) return true;
    if (isSVGPropValue(elem, propName)) return false;
    return true;
};
const setHtmlProperty = (props)=>(elem, propName)=>{
        const htmlPropName = mapPropName(propName);
        const htmlPropValue = resolvePropValue(props, propName);
        isHtmlPropName(elem, htmlPropName) && isWritableHtmlProp(elem, propName) ? elem[htmlPropName] = htmlPropValue : setAttribute(elem, htmlPropName, htmlPropValue);
        return elem;
    };
const setHtmlProperties = (elem, props)=>getValidPropNames(elem, props).reduce(setHtmlProperty(props), elem);
const removeAttribute = (elem, attrName)=>elem.removeAttribute(attrName);
const unsetHtmlProperty = (elem, propName)=>{
    const htmlPropName = mapPropName(propName);
    isHtmlPropName(elem, htmlPropName) && isWritableHtmlProp(elem, propName) ? elem[htmlPropName] = undefined : removeAttribute(elem, htmlPropName);
    return elem;
};
const unsetHtmlProperties = (elem, props)=>getValidPropNames(elem, props).reduce(unsetHtmlProperty, elem);
const createHtmlText = (document, text)=>document.createTextNode(text);
const isHtmlText = (elem)=>elem.nodeType === 3;
const getHtmlText = ($elem)=>isHtmlText($elem) && $elem.textContent;
const setHtmlText = ($elem, text)=>$elem.textContent = text;
const getJsxFactoryName = (elem)=>elem.type.name.toLowerCase();
const isJsxArrayElems = (elems)=>elems instanceof Array;
const isJsxFactory = (elem)=>typeof elem.type === "function";
const getJsxFragmentName = ()=>"fragment";
const FragmentType = Symbol.for("react.fragment");
const isJsxFragment = (elem)=>elem?.type === FragmentType;
const isBoolean = (value)=>typeof value === "boolean";
const isNull = (value)=>value === null;
const isUndefined = (value)=>typeof value === "undefined";
const isJsxText = (value)=>value?.$$typeof === undefined;
const isValidJsxText = (value)=>!isBoolean(value) && !isNull(value) && !isUndefined(value);
const getJsxText = (value)=>isJsxText(value) && value?.toString();
const getJsxTextName = ()=>"text";
const ElementType = Symbol.for("react.element");
const SafeTypes = Object.freeze([
    ElementType,
    FragmentType
]);
const isJsxElement = (elem)=>typeof elem.type === 'string';
const isJsxKeyElement = (elem)=>elem.key != undefined;
const isJsxType = (elem)=>typeof elem.$$typeof === "symbol";
const isSafeJsxElement = (elem)=>typeof elem.$$typeof === "symbol" ? SafeTypes.includes(elem.$$typeof) : true;
const getJsxElement = (store)=>store.__elem;
const getJsxElementKey = (elem)=>elem.key;
const getJsxElementName = (elem)=>elem.type;
const getJsxElementProps = (elem)=>elem.props;
const getJsxElementType = (type)=>typeof type === 'symbol' ? type : ElementType;
const getJsxName = (elem)=>isJsxFactory(elem) && getJsxFactoryName(elem) || isJsxElement(elem) && getJsxElementName(elem) || isJsxFragment(elem) && getJsxFragmentName() || getJsxTextName();
const createJsxElement = (type, props, key, parent, ref)=>({
        $$typeof: getJsxElementType(type),
        type,
        props,
        key,
        ref,
        _owner: parent
    });
const getJsxParent = (internals)=>internals?.ReactCurrentOwner?.current;
const getJsxInternals = (store)=>store?.__SECRET_INTERNALS_DO_NOT_USE_OR_YOU_WILL_BE_FIRED;
const ResevedPropNames = Object.freeze({
    key: undefined,
    ref: undefined,
    __self: undefined,
    __source: undefined
});
const existsJsxKey = (key)=>key !== undefined;
const getJsxPropsKey = (props)=>props.key;
const existsJsxPropsKey = (props)=>getJsxPropsKey(props) !== undefined;
const isArrayPropsChildren = (props)=>props.children instanceof Array;
const getJsxPropsChildren = (props)=>isArrayPropsChildren(props) ? props.children : [
        props.children
    ];
const getJsxPropNames = (props)=>Object.getOwnPropertyNames(props);
const getJsxPropsRef = (props)=>props.ref;
const existsJsxPropsRef = (props)=>getJsxPropsRef(props) !== undefined;
const existsJsxPropValue = (props, propName)=>props[propName] !== undefined;
const isReservedJsxPropName = (propName)=>propName in ResevedPropNames;
const setJsxPropValue = (props, propName, propValue)=>props[propName] = propValue;
const copyJsxProp = (sourceProps)=>(targetProps, propName)=>{
        setJsxPropValue(targetProps, propName, sourceProps[propName]);
        return targetProps;
    };
const copyDefaultJsxProps = (sourceProps, targetProps)=>getJsxPropNames(sourceProps).filter((propName)=>!existsJsxPropValue(targetProps, propName)).reduce(copyJsxProp(sourceProps), targetProps);
const copyValidJsxProps = (sourceProps, targetProps = {})=>getJsxPropNames(sourceProps).filter((propName)=>!isReservedJsxPropName(propName)).reduce(copyJsxProp(sourceProps), targetProps);
const resolveJsxPropsyKey = (props, maybeKey)=>existsJsxPropsKey(props) && getJsxPropsKey(props).toString() || existsJsxKey(maybeKey) && maybeKey.toString() || null;
const resolveJsxPropsRef = (props)=>existsJsxPropsRef(props) && getJsxPropsRef(props) || null;
const resolveJsxProps = (initialProps, type)=>type && type.defaultProps ? copyDefaultJsxProps(type.defaultProps, copyValidJsxProps(initialProps)) : copyValidJsxProps(initialProps);
const getJsxLegacyChildren = (children)=>children?.length == 1 ? children[0] : children;
const emptyLegacyJsxChildren = (children)=>!children || children.length === 0;
const compileJsxExpression = (type, props, maybeKey)=>createJsxElement(type, resolveJsxProps(props, type), resolveJsxPropsyKey(props, maybeKey), getJsxParent(getJsxInternals(globalThis["React"])), resolveJsxPropsRef(props));
const compileLegacyJsxExpression = (type, props, ...children)=>emptyLegacyJsxChildren(children) ? compileJsxExpression(type, props ?? {}) : compileJsxExpression(type, {
        ...props ?? {},
        children: getJsxLegacyChildren(children)
    });
const jsx = compileJsxExpression;
const jsxs = compileJsxExpression;
const createElement = compileLegacyJsxExpression;
export { jsx as jsx };
export { jsxs as jsxs };
export { createElement as createElement };
export { FragmentType as Fragment };
const throwError = (message)=>{
    if (!message) return false;
    throw new Error(message);
};
const replaceJsxFragments = (elems, firstElem = elems[0])=>isJsxFragment(firstElem) ? getJsxPropsChildren(firstElem.props) : elems;
const sanitizeJsxChildren = (elem)=>sanitizeJsxElements(getJsxPropsChildren(elem.props));
const sanitizeJsxElements = (elems)=>replaceJsxFragments(elems).filter((elem)=>isValidJsxText(elem) && isSafeJsxElement(elem));
const storeJsxElement = (store, elem)=>store.__elem = elem;
const validateJsxElement = (elem)=>isJsxType(elem) ? "" : "Element should be jsx element.";
const sanitizeJsxPropsChildren = (props, children)=>({
        ...props,
        children: sanitizeJsxElements(children)
    });
const runJsxFactory = (elem, $elem, props)=>elem.type(Object.freeze(props), $elem);
const buildJsxFactoryChildren = (elem, $elem)=>{
    const children = getJsxPropsChildren(elem.props);
    const sanitizeProps = sanitizeJsxPropsChildren(elem.props, children);
    const factoryElems = runJsxFactory(elem, $elem, sanitizeProps);
    return sanitizeJsxElements(isJsxArrayElems(factoryElems) ? factoryElems : [
        factoryElems
    ]);
};
const isLogCategoryEnabled = (elem, category)=>elem.__log.includes(category);
const isLogMounted = (elem)=>elem.__log instanceof Array;
const isLogEnabled = (elem, category)=>isLogMounted(elem) && isLogCategoryEnabled(elem, category);
const mountLog = ($elem, $parent)=>$elem.__log = [
        ...$parent.__log
    ];
const enableLogging = ($elem, $parent)=>isLogMounted($elem) || isLogMounted($parent) && mountLog($elem, $parent);
const Category = "rendering";
const LogHeader = "[rendering]";
const logError = (elem, ...args)=>isLogEnabled(elem, Category) && console.error(LogHeader, ...args);
const logInfo = (elem, ...args)=>isLogEnabled(elem, Category) && console.info(LogHeader, ...args);
const isIgnoreArray = (elem)=>elem.__ignore instanceof Array;
const isIgnoredElement = ($elem)=>$elem.__ignore?.includes(getHtmlName($elem));
const enableIgnoring = ($elem, $parent)=>isIgnoreArray($parent) && ($elem.__ignore = [
        ...$parent.__ignore
    ]);
const storeInternals = ($elem, elem)=>storeJsxElement($elem, elem);
const getElementNS = (elem)=>getJsxElementProps(elem).xmlns;
const getHtmlElementNS = ($elem)=>$elem && getJsxElement($elem) && getJsxElementProps(getJsxElement($elem)).xmlns;
const getHtmlPropNames = ($elem)=>Object.getOwnPropertyNames($elem);
const setHtmlElement = ($elem, elem)=>{
    setHtmlProperties($elem, getJsxElementProps(elem));
    setEventHandlers($elem, getJsxElementProps(elem));
    return $elem;
};
const renderHtmlElement = (elem, $parent)=>{
    throwError(validateHtmlTagName(getJsxName(elem)));
    const document = getHtmlOwnerDocument($parent);
    const ns = getElementNS(elem) || getHtmlElementNS($parent);
    const $elem = ns ? createHtmlElementNS(document, ns, getJsxName(elem)) : createHtmlElement(document, getJsxName(elem));
    setHtmlElement($elem, elem);
    appendHtmlNode($elem, $parent);
    enableIgnoring($elem, $parent);
    enableLogging($elem, $parent);
    storeInternals($elem, elem);
    return $elem;
};
const updateHtmlElement = (elem, $elem)=>{
    setHtmlProperties($elem, getJsxElementProps(elem));
    setEventHandlers($elem, getJsxElementProps(elem));
    storeInternals($elem, elem);
    return $elem;
};
const isInternalName = (name)=>name.startsWith("__");
const unstoreInternal = (elem, propName)=>{
    delete elem[propName];
    return elem;
};
const unstoreInternals = ($elem)=>getHtmlPropNames($elem).filter(isInternalName).reduce(unstoreInternal, $elem);
const unsetHtmlElement = ($elem)=>{
    const props = getJsxElementProps(getJsxElement($elem));
    unsetHtmlProperties($elem, props);
    unsetEventHandlers($elem, props);
    return $elem;
};
const unrenderHtmlElement = ($elem)=>{
    unsetHtmlElement($elem);
    unstoreInternals($elem);
    return getHtmlParentElement($elem) ? removeHtmlNode($elem) : $elem;
};
const logHtmlElement = ($elem, $parent, message)=>logInfo($elem, message, "elem:", getHtmlName($elem), "props:", getJsxElementProps(getJsxElement($elem)), "parent:", $parent && getHtmlName($parent));
const existsElement = (elem)=>elem;
const insertHtmlText = (text, $elem, $parent)=>{
    const document = getHtmlOwnerDocument($parent);
    const $text = createHtmlText(document, text);
    return insertHtmlNode($text, $elem);
};
const equalElementsKeys = (elem, $elem)=>getJsxElementKey(elem) === getJsxElementKey(getJsxElement($elem));
const findElementsByKey = (elem, $elems)=>$elems.find(($elem)=>equalElementsKeys(elem, $elem));
const orderElementKey = ($source, $target, $parent)=>$target === $source && $source || $source && $target && insertHtmlNode($target, $source) || $source && insertHtmlText("", $source, $parent);
const orderElementKeys = (elems, $elems, $parent)=>{
    elems.forEach((elem, index)=>orderElementKey(getHtmlChildNode($parent, index), findElementsByKey(elem, $elems), $parent));
    return getHtmlChildNodes($parent);
};
const logHtmlText = ($text, $parent, message)=>logInfo($text, message, "text:", getHtmlText($text), "parent:", $parent && getHtmlName($parent));
const renderHtmlText = (text, $parent)=>{
    const document = getHtmlOwnerDocument($parent);
    const $text = createHtmlText(document, text);
    return appendHtmlNode($text, $parent);
};
const updateHtmlText = (text, $elem)=>{
    setHtmlText($elem, text);
    return $elem;
};
const unrenderHtmlText = ($elem)=>getHtmlParentElement($elem) ? removeHtmlNode($elem) : $elem;
const getLogger = ($elem)=>isHtmlText($elem) ? logHtmlText : logHtmlElement;
const logElement = ($elem, message)=>getLogger($elem)($elem, getHtmlParentElement($elem), message);
const dispatchError = (elem, error)=>dispatchEvent(elem, "error", {
        error
    });
const handleError = (func, elem)=>{
    try {
        return func();
    } catch (error) {
        logError(elem, error.message, error.stack);
        dispatchError(elem, error);
        throw error;
    }
};
const resolveJsxChildren = (elem, $elem)=>isJsxFactory(elem) && buildJsxFactoryChildren(elem, $elem) || isJsxElement(elem) && sanitizeJsxChildren(elem) || [];
const resolveHtmlChildren = ($elem, children)=>existsElement(children[0]) && isJsxKeyElement(children[0]) ? orderElementKeys(children, getHtmlChildNodes($elem), $elem) : getHtmlChildNodes($elem);
const equalPrimitives = (value1, value2)=>value1 === value2;
const falsy = ()=>false;
const truthy = ()=>true;
const getObjectPropNames = (obj)=>Object.getOwnPropertyNames(obj);
const countObjectProps = (obj)=>Object.getOwnPropertyNames(obj).length;
const ReservedPropNames1 = Object.freeze([
    "children"
]);
const equalObjectsPropsCount = (obj1, obj2)=>countObjectProps(obj1) === countObjectProps(obj2);
const existsObject = (obj)=>obj != null;
const existsObjects = (obj1, obj2)=>existsObject(obj1) && existsObject(obj2);
const isObjectType = (value)=>typeof value === "object" && value !== null;
const isReservedObjectPropName = (propName)=>ReservedPropNames1.includes(propName);
const equalArraysLength = (arr1, arr2)=>arr1.length === arr2.length;
const existsArray = (arr)=>arr != null;
const existsArrays = (arr1, arr2)=>existsArray(arr1) && existsArray(arr2);
const isArrayType = (value)=>value instanceof Array;
const isFunctionType = (value)=>typeof value === "function";
const equalArrayItems = (arr1, arr2)=>arr1.every((_, index)=>equalValues(arr1[index], arr2[index]));
const equalArrays = (arr1, arr2)=>(!existsArrays(arr1, arr2) && equalPrimitives || !equalArraysLength(arr1, arr2) && falsy || equalArrayItems)(arr1, arr2);
const equalValues = (value1, value2)=>(isFunctionType(value1) && isFunctionType(value2) && truthy || isArrayType(value1) && isArrayType(value2) && equalArrays || isObjectType(value1) && isObjectType(value2) && equalObjects || equalPrimitives)(value1, value2);
const equalObjectsProp = (obj1, obj2, propName)=>isReservedObjectPropName(propName) || equalValues(obj1[propName], obj2[propName]);
const equalObjectsProps = (obj1, obj2)=>getObjectPropNames(obj1).every((propName)=>equalObjectsProp(obj1, obj2, propName));
const equalObjects = (obj1, obj2)=>(!existsObjects(obj1, obj2) && equalPrimitives || !equalObjectsPropsCount(obj1, obj2) && falsy || equalObjectsProps)(obj1, obj2);
const equalElementNames = (elem, $elem)=>getJsxName(elem) === getHtmlName($elem);
const equalElementProps = (elem, $elem)=>!getJsxElementProps(elem)["no-skip"] && equalObjects(getJsxElementProps(elem), getJsxElementProps(getJsxElement($elem)));
const equalElementTexts = (elem, $elem)=>getJsxText(elem) === getHtmlText($elem);
const isStyleElement = (elem)=>getHtmlName(elem) === "style";
const isUpdatedElement = ($elem)=>isHtmlElement($elem);
const shouldSkipElement = ($elem)=>isStyleElement($elem) || isIgnoredElement($elem) || isHtmlText($elem);
const shouldRenderElement = ($elem)=>!existsElement($elem);
const shouldReplaceElement = (elem, $elem)=>!equalElementNames(elem, $elem);
const shouldUnrenderElement = (elem)=>!existsElement(elem);
const shouldUpdateElement = (elem, $elem)=>equalElementNames(elem, $elem) && (isJsxElement(elem) || isJsxFactory(elem) && !equalElementProps(elem, $elem) || isJsxText(elem) && !equalElementTexts(elem, $elem));
const renderElement = (elem, $parent)=>(isJsxText(elem) ? renderHtmlText : renderHtmlElement)(elem, $parent);
const renderElements = (elem, $parent = parseHtml("<main></main>"))=>{
    isJsxText(elem) || throwError(validateHtmlElement($parent));
    isJsxText(elem) || throwError(validateJsxElement(elem));
    const rendered = [
        renderElement(elem, $parent)
    ];
    for (const $elem of rendered){
        logElement($elem, "render");
        shouldSkipElement($elem) || handleError(()=>resolveJsxChildren(getJsxElement($elem), $elem), $elem).forEach((child)=>rendered.push(renderElement(child, $elem)));
    }
    return rendered;
};
const replaceElement = ($elem, $oldElem)=>(logElement($oldElem, "replace"), isHtmlText($oldElem) ? replaceHtmlNode($elem, $oldElem) : replaceHtmlNode($elem, $oldElem), $elem);
const getEffect = (effects, name)=>effects[name];
const getEffects = (elem)=>elem.__effects;
const runInitialFunc = (effect)=>effect.initialFunc?.();
const runInitialEffects = (effects)=>effects ? Object.values(effects).map(runInitialFunc) : [];
const setEffectDeps = (effect, deps)=>effect.deps = deps;
const setEffectInitialFunc = (effect, func)=>effect.initialFunc = func;
const setEffect = (effects, effect)=>effects[effect.name] = effect;
const setEffects = (elem, effects = {})=>elem.__effects = elem.__effects ?? effects;
const setInitialEffect = (effects, name, func)=>setEffectInitialFunc(getEffect(effects, name), func);
const createEffect = (name, deps)=>({
        name,
        deps
    });
const existsEffect = (effects, name)=>effects[name];
const isDefaultDeps = (deps)=>deps === undefined;
const useEffect = (effects, name, func, deps)=>{
    if (!existsEffect(effects, name)) {
        setEffect(effects, createEffect(name, deps));
        return func();
    }
    const effect = getEffect(effects, name);
    runInitialFunc(effect);
    setEffectInitialFunc(effect, undefined);
    if (equalArrays(effect.deps, deps) && !isDefaultDeps(deps)) return;
    setEffectDeps(effect, deps);
    return func();
};
const unrenderElement = ($elem)=>(logElement($elem, "unrender"), isHtmlText($elem) ? unrenderHtmlText($elem) : (runInitialEffects(getEffects($elem)), unrenderHtmlElement($elem)));
const unrenderElements = ($elem)=>{
    isHtmlText($elem) || throwError(validateHtmlElement($elem));
    const unrendered = [
        unrenderElement($elem)
    ];
    for (const $elem of unrendered)shouldSkipElement($elem) || getHtmlChildNodes($elem).forEach(($child)=>unrendered.push(unrenderElement($child)));
    return unrendered;
};
const getMaxLengthElements = (elems, $elems)=>elems.length > $elems.length ? elems : $elems;
const updateElement = (elem, $elem)=>(logElement($elem, "update"), (isJsxText(elem) ? updateHtmlText : updateHtmlElement)(elem, $elem));
const reconcileElement = (elem, $elem, $parent)=>shouldRenderElement($elem) && renderElements(elem, $parent) || shouldUnrenderElement(elem) && unrenderElements($elem) || shouldUpdateElement(elem, $elem) && updateElement(elem, $elem) || shouldReplaceElement(elem, $elem) && [
        replaceElement(renderElements(elem, $parent)[0], $elem),
        unrenderElements($elem)
    ];
const updateElements = ($elem, elem = getJsxElement($elem))=>{
    isJsxText(elem) || throwError(validateHtmlElement($elem));
    isJsxText(elem) || throwError(validateJsxElement(elem));
    const updated = [
        updateElement(elem, $elem)
    ];
    for (const $elem of updated){
        if (shouldSkipElement($elem)) continue;
        const children = handleError(()=>resolveJsxChildren(getJsxElement($elem), $elem), $elem);
        const $children = resolveHtmlChildren($elem, children);
        getMaxLengthElements(children, $children).map((_, index)=>reconcileElement(children[index], $children[index], $elem)).filter(($elem)=>isUpdatedElement($elem)).forEach(($elem)=>updated.push($elem));
    }
    return updated;
};
const render = (elem, $parent = parseHtml("<main></main>"))=>{
    $parent.ownerDocument.__render = $parent.ownerDocument.__render || renderElements;
    $parent.ownerDocument.__update = $parent.ownerDocument.__update || updateElements;
    $parent.ownerDocument.__unrender = $parent.ownerDocument.__unrender || unrenderElements;
    return renderElements(elem, $parent)[0];
};
export { updateElements as update };
export { unrenderElements as unrender };
export { render as render };
const setContext = (contexts, context)=>contexts[context.name] = context;
const setContexts = (elem, contexts = {})=>elem.__contexts = elem.__contexts ?? contexts;
const setContextValue = (context, value)=>(context.value = value, context);
const createContext = (name, value)=>({
        name,
        value
    });
const getContext = (contexts, name)=>contexts[name];
const getContexts = (elem)=>elem.__contexts;
const existsContext = (contexts, name)=>name in contexts;
const isContextConsumer = (elem, name)=>getHtmlName(elem) !== "context" && existsContext(getContexts(elem), name);
const isContextProducer = (elem, name)=>getHtmlName(elem) === "context" && existsContext(getContexts(elem), name);
const findProducer = (elem, name)=>findHtmlAscendant(elem, (elem)=>isContextProducer(elem, name));
const getContextValue = (contexts, name)=>getContext(contexts, name).value;
const getProducerContextValue = (name, fallbackValue, elem)=>{
    const producer = findProducer(elem, name);
    if (!producer) return fallbackValue;
    const contexts = getContexts(producer);
    const context = getContext(contexts, name);
    return context.value;
};
const findConsumer = (elem, name)=>findHtmlDescendants(elem, (elem)=>isContextConsumer(elem, name));
const updateConsumerContext = (name, value, elem)=>{
    const contexts = getContexts(elem);
    const context = getContext(contexts, name);
    if (equalValues(context.value, value)) return;
    setContextValue(context, value);
    return updateElements(elem);
};
const updateProducerContext = (name, value, elem)=>{
    const contexts = getContexts(elem);
    const context = getContext(contexts, name);
    return setContextValue(context, value);
};
const updateContexts = (name, value, elem)=>{
    const producer = findProducer(elem, name);
    updateProducerContext(name, value, producer);
    return findConsumer(producer, name).map((consumer)=>updateConsumerContext(name, value, consumer));
};
const useContext = (contexts, name, initialValue, elem)=>{
    if (!existsContext(contexts, name)) {
        const contextValue = getProducerContextValue(name, initialValue, elem);
        const context = createContext(name, contextValue);
        setContext(contexts, context);
    }
    return [
        getContextValue(contexts, name),
        (value)=>updateContexts(name, value, elem)
    ];
};
const Context = ({ name, value, children }, elem)=>{
    const [, setContext] = useContext(setContexts(elem), name, value, elem);
    useEffect(setEffects(elem), "setcontext", ()=>setContext(value, elem), [
        value
    ]);
    return children;
};
const getErrorPath = (boundary, elem, names = [])=>{
    if (!elem) return;
    names.push(getHtmlName(elem));
    return boundary === elem ? names.reverse().join("/") : getErrorPath(boundary, elem.parentElement, names);
};
const ErrorBoundary = ({ path, error, children }, elem)=>{
    setEventHandler(elem, "onerror", (event)=>{
        event.stopPropagation();
        return updateErrorBoundary(elem, event);
    });
    return error ? React.createElement("error", null, React.createElement("span", {
        class: "path"
    }, `Path: ${path}`), React.createElement("pre", {
        class: "error"
    }, `Error: ${error}`)) : children;
};
const updateErrorBoundary = (elem, event)=>{
    const path = getErrorPath(elem, event.target);
    const error = event.detail?.error;
    return updateElements(elem, React.createElement(ErrorBoundary, {
        path: path,
        error: error?.message
    }));
};
const createMemo = (name, value, deps)=>({
        name,
        value,
        deps
    });
const setMemo = (states, memo)=>states[memo.name] = memo;
const setMemoDeps = (memo, deps)=>memo.deps = deps;
const setMemoValue = (memo, value)=>memo.value = value;
const getMemo = (memos, name)=>memos[name];
const getMemoUsage = (memo)=>[
        memo.value,
        (func)=>setMemoValue(memo, func())
    ];
const existsMemo = (states, name)=>states[name];
const isDefaultDeps1 = (deps)=>deps === undefined;
const useMemo = (states, name, func, deps)=>{
    if (!existsMemo(states, name)) {
        const memo = setMemo(states, createMemo(name, func(), deps));
        return getMemoUsage(memo);
    }
    const memo = getMemo(states, name);
    if (equalArrays(memo.deps, deps) && !isDefaultDeps1(deps)) return getMemoUsage(memo);
    setMemoDeps(memo, deps);
    setMemoValue(memo, func());
    return getMemoUsage(memo);
};
const setState = (states, state)=>states[state.name] = state;
const setStateDeps = (state, deps)=>state.deps = deps;
const setStateValue = (state, value)=>state.value = value;
const setStates = (elem, states = {})=>elem.__states = elem.__states ?? states;
const getState = (states, name)=>states[name];
const getStateUsage = (state)=>[
        state.value,
        (value)=>setStateValue(state, value)
    ];
const createState = (name, value, deps)=>({
        name,
        value,
        deps
    });
const existsState = (states, name)=>states[name];
const isDefaultDeps2 = (deps)=>deps === undefined;
const useState = (states, name, value, deps)=>{
    if (!existsState(states, name)) {
        const state = setState(states, createState(name, value, deps));
        return getStateUsage(state);
    }
    const state = getState(states, name);
    if (equalArrays(state.deps, deps) && !isDefaultDeps2(deps)) return getStateUsage(state);
    setStateDeps(state, deps);
    setStateValue(state, value);
    return getStateUsage(state);
};
const isFunctionLazyLoader = (loader)=>typeof loader === "function";
const validateLazyLoader = (loader)=>isFunctionLazyLoader(loader) ? "" : "Lazy loader should be function.";
const Lazy = (props, elem)=>{
    throwError(validateHtmlElement(elem));
    throwError(validateLazyLoader(props.loader));
    const states = setStates(elem);
    const effects = setEffects(elem);
    const [factory, setFactory] = useState(states, "factory", undefined, []);
    if (factory) return createJsxElement(factory, props);
    useEffect(effects, "load", async ()=>{
        const factory = await props.loader();
        setFactory(factory);
        render(createJsxElement(factory, props), elem);
    });
};
const setServices = (elem, services)=>elem.ownerDocument.__services = services;
const Services = (props, elem)=>{
    setServices(elem, props);
    return props.children;
};
const getService = (elem, name, fallback)=>getServices(elem)?.[name] ?? fallback;
const getServices = (elem)=>elem.ownerDocument.__services;
const setHiddenProps = (elem, value)=>elem.props.hidden = value;
const Suspense = ({ suspending = true, fallback, children })=>{
    setHiddenProps(fallback, !suspending);
    children.forEach((child)=>setHiddenProps(child, suspending));
    return React.createElement(React.Fragment, null, fallback, ...children);
};
export { Context as Context };
export { getContexts as getContexts };
export { setContexts as setContexts };
export { useContext as useContext };
export { ErrorBoundary as ErrorBoundary };
export { Lazy as Lazy };
export { Services as Services };
export { getService as getService };
export { Suspense as Suspense };
export { dispatchEvent as dispatchEvent };
export { setEventHandler as setEventHandler };
try {
    globalThis["DOMParser"] || await registerDOMParser();
    globalThis["React"] = globalThis["React"] ?? {};
    globalThis["React"].createElement = createElement;
    globalThis["React"].Fragment = FragmentType;
} catch (error) {
    console.error(error);
    throw error;
}
export { setEffects as setEffects, useEffect as useEffect, setInitialEffect as setInitialEffect };
export { setStates as setStates, useMemo as useMemo, useState as useState };
