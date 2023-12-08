// deno-fmt-ignore-file
// deno-lint-ignore-file
// This code was bundled using `deno bundle` and it's not recommended to edit it manually

const getHtmlName = (elem)=>elem.tagName?.toLowerCase() || "text";
const getHtmlParentElement = (elem)=>elem?.parentElement;
const storeEventHandler = (elem, handlerName, handler)=>elem[handlerName] = handler;
const listenForEvent = (elem, eventName, handler)=>{
    const handlerName = `on${eventName}`;
    elem.removeEventListener(eventName, elem[handlerName]);
    storeEventHandler(elem, handlerName, handler);
    elem.addEventListener(eventName, handler);
    return elem;
};
const findHtmlAscendant = (elem, func)=>{
    while(elem){
        if (func(elem)) return elem;
        elem = elem.parentElement;
    }
};
const findHtmlDescendants = (elem, func = (e)=>e)=>{
    const elems = [
        elem
    ];
    const result = [];
    for (const elem1 of elems){
        if (func(elem1)) result.push(elem1);
        elems.push(...Array.from(elem1.children));
    }
    return result;
};
const isEventHandler = (name)=>name.startsWith("on");
const jsProtocolRegex = /^[\u0000-\u001F ]*j[\r\n\t]*a[\r\n\t]*v[\r\n\t]*a[\r\n\t]*s[\r\n\t]*c[\r\n\t]*r[\r\n\t]*i[\r\n\t]*p[\r\n\t]*t[\r\n\t]*\:/i;
const unsafeElements = [
    "SCRIPT",
    "IFRAME"
];
const unsafeProperties = [
    "style",
    "css",
    "innerHTML",
    "outerHTML"
];
const urlProperties = [
    "action",
    "background",
    "dynsrv",
    "href",
    "lowsrc",
    "src"
];
const isFunctionProperty = (props, name)=>typeof props[name] === "function";
const isSafeTagPropName = (tagName, name)=>tagName === "style" && name === "css";
const isSafeElement = (name)=>!unsafeElements.includes(name.toUpperCase());
const isSafeEventHandler = (props)=>(propName)=>isEventHandler(propName) && isFunctionProperty(props, propName);
const isSafeProperty = (tagName)=>(propName)=>isSafeTagPropName(tagName, propName) || !unsafeProperties.includes(propName);
const isSafeUrl = (props)=>(propName)=>urlProperties.includes(propName) ? jsProtocolRegex.test(props[propName] || "") === false : true;
const getValidEventHandlers = (props)=>Object.getOwnPropertyNames(props || {}).filter(isEventHandler).filter(isSafeEventHandler(props));
const getEventHame = (handlerName)=>handlerName.replace("on", "");
const registerEventHandler = (elem, props)=>(handlerName)=>listenForEvent(elem, getEventHame(handlerName), props[handlerName]);
const registerEventHandlers = (elem, props)=>getValidEventHandlers(props).map(registerEventHandler(elem, props));
const isEmptyPropertyValue = (propValue)=>propValue == undefined || propValue === "";
const isHtmlName = (propName)=>propName === "html";
const isPropertyName = (propName)=>!isEventHandler(propName);
const getValidProperties = (tagName, props)=>Object.getOwnPropertyNames(props || {}).filter(isPropertyName).filter(isSafeProperty(tagName)).filter(isSafeUrl(props));
const ariaToCamelCase = (string)=>`aria${string[5].toUpperCase()}${string.substring(6)}`;
const ariaMappings = Object.freeze({
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
const specialMappings = Object.freeze({
    class: "className",
    for: "htmlFor",
    readonly: "readOnly",
    tabindex: "tabIndex",
    css: "innerHTML",
    html: "innerHTML"
});
const isAriaPropName = (propName)=>propName.startsWith("aria-");
const isSpecialPropName = (propName)=>specialMappings[propName];
const MappingType = Object.freeze({
    aria: 0,
    special: 1,
    regular: 2
});
const getMappingType = (propName)=>{
    if (isAriaPropName(propName)) return MappingType.aria;
    if (isSpecialPropName(propName)) return MappingType.special;
    return MappingType.regular;
};
const mapPropertyName = (propName)=>{
    const mappingType = getMappingType(propName);
    switch(mappingType){
        case MappingType.aria:
            return ariaMappings[propName] || ariaToCamelCase(propName);
        case MappingType.special:
            return specialMappings[propName];
        case MappingType.regular:
            return propName;
        default:
            throw new Error(`Unsupported mapping type ${mappingType}.`);
    }
};
const encodingCharsRegex = /[^\w. ]/gi;
const getHtmlEntity = (__char)=>`&#${__char.charCodeAt(0)};`;
const encodeHtml = (string)=>string.replace(encodingCharsRegex, getHtmlEntity);
const togglePropNames = [
    "checked",
    "disabled",
    "hidden",
    "readOnly",
    "selected"
];
const isEmptyToggleProperty = (propName, propValue)=>togglePropNames.includes(propName) && isEmptyPropertyValue(propValue);
const PropValueType = Object.freeze({
    emptyToggle: 0,
    html: 1,
    regular: 2
});
const getPropValueType = (propName, propValue)=>{
    if (isEmptyToggleProperty(propName, propValue)) return PropValueType.emptyToggle;
    if (isHtmlName(propName)) return PropValueType.html;
    return PropValueType.regular;
};
const resolvePropertyValue = (props, propName)=>{
    const propValue = props[propName];
    const propValueType = getPropValueType(propName, propValue);
    switch(propValueType){
        case PropValueType.emptyToggle:
            return true;
        case PropValueType.html:
            return encodeHtml(propValue);
        case PropValueType.regular:
            return propValue;
        default:
            throw new Error(`Unsupported prop value type ${propValueType}.`);
    }
};
const renderProperty = (props)=>(elem, propName)=>{
        elem[mapPropertyName(propName)] = resolvePropertyValue(props, propName);
        return elem;
    };
const renderProperties = (elem, props)=>getValidProperties(getHtmlName(elem), props).reduce(renderProperty(props), elem);
const isHtmlElement = (elem)=>elem.constructor?.name.endsWith("Element");
const validateHtmlElement = (elem, name)=>{
    if (isHtmlElement(elem)) return elem;
    throw new Error(`${name} should be HTML Element.`);
};
const validateElementName = (elem)=>{
    if (isSafeElement(elem.name)) return elem.name;
    throw new Error(`Unsafe html element '${elem.name}'.`);
};
const cloneHtmlNode = (node)=>node.cloneNode(false);
const memoizeHtmlNode = (parser)=>{
    const cache = new Map();
    return (nodeName)=>{
        cache[nodeName] = cache[nodeName] || parser(nodeName);
        if (!cache[nodeName]) throw `Cloning '${nodeName}' node name failed.`;
        return cloneHtmlNode(cache[nodeName]);
    };
};
let _parser = undefined;
const mimeType = "text/html";
const parser = ()=>_parser = _parser || new DOMParser();
const parseRootHtml = (html)=>new DOMParser().parseFromString(html, mimeType)["body"].children[0];
const parseHtml = (html)=>parser().parseFromString(html, mimeType)["body"].children[0];
const parseStyleHtml = (html)=>parser().parseFromString(html, mimeType).querySelector("STYLE");
const createHtmlElement = memoizeHtmlNode((tagName)=>tagName.toLowerCase() === "style" ? parseStyleHtml(`<style></style>`) : parseHtml(`<${tagName}></${tagName}>`));
const buildHtmlElement = (elem)=>{
    const name = validateElementName(elem);
    const $elem = createHtmlElement(name);
    renderProperties($elem, elem.props);
    registerEventHandlers($elem, elem.props);
    return $elem;
};
const getEventHame1 = (handlerName)=>handlerName.replace("on", "");
const unregisterEventHandler = (elem)=>(handlerName)=>{
        elem.removeEventListener(getEventHame1(handlerName), elem[handlerName]);
        delete elem[handlerName];
        return elem;
    };
const unregisterEventHandlers = (elem, props)=>getValidEventHandlers(props).forEach(unregisterEventHandler(elem));
const unrenderProperty = (elem)=>(propName)=>{
        elem[mapPropertyName(propName)] = undefined;
        return elem;
    };
const unrenderProperties = (elem, props)=>getValidProperties(getHtmlName(elem), props).forEach(unrenderProperty(elem));
const destroyHtmlElement = ($elem, props)=>{
    unrenderProperties($elem, props);
    unregisterEventHandlers($elem, props);
    return $elem;
};
const updateProperty = (props)=>(elem, propName)=>{
        elem[mapPropertyName(propName)] = resolvePropertyValue(props, propName);
        return elem;
    };
const updateProperties = (elem, props)=>getValidProperties(getHtmlName(elem), props).reduce(updateProperty(props), elem);
const updateHtmlElement = ($elem, elem)=>{
    $elem = updateProperties($elem, elem.props);
    registerEventHandlers($elem, elem.props);
    return $elem;
};
const deno_dom_url = "https://deno.land/x/deno_dom@v0.1.35-alpha/deno-dom-wasm.ts";
const registerDOMParser = async (url = deno_dom_url)=>globalThis.DOMParser = globalThis.DOMParser || (await import(url)).DOMParser;
const appendHtmlNode = (node, parent)=>parent.appendChild(node);
const insertHtmlNode = (node, oldNode)=>oldNode.parentNode.insertBefore(node, oldNode) && node;
const getHtmlChildNodes = (node)=>Array.from(node.childNodes);
const removeHtmlNode = (node)=>node.parentElement.removeChild(node);
const replaceHtmlNode = (node, oldNode)=>oldNode.parentNode.replaceChild(node, oldNode) && node;
const getStoredProps = (store)=>store.__props;
const storeProps = (store, props)=>store.__props = props;
const unstoreProps = (store)=>delete store.__props;
const isHtmlText = (elem)=>elem.nodeType === 3;
const getHtmlText = ($elem)=>isHtmlText($elem) && $elem.textContent;
let _parser1 = undefined;
const defaultHtml = "<body></body>";
const mimeType1 = "text/html";
const parser1 = ()=>_parser1 = _parser1 || new DOMParser();
const parseHtmlText = (text)=>parser1().parseFromString(defaultHtml, mimeType1).createTextNode(text);
const createHtmlText = memoizeHtmlNode(parseHtmlText);
const buildHtmlText = (elem)=>createHtmlText(elem.text);
const updateHtmlText = ($elem, elem)=>$elem.textContent !== elem.text ? parseHtmlText(elem.text) : $elem;
const getJsxChildren = (elem)=>elem.children || [];
const getJsxName = (elem)=>elem.name.toLowerCase();
const JsxType = Object.freeze({
    factory: 0,
    element: 1,
    text: 2,
    fragment: 3
});
const isJsxFragment = (element)=>element.type === JsxType.fragment;
const isJsxFragmentTag = (factory)=>factory === undefined;
const replaceJsxFragment = (element)=>element && isJsxFragment(element) ? element.children : element;
const createJsxText = (value)=>Object.freeze({
        name: "text",
        type: JsxType.text,
        text: value?.toString()
    });
const isJsxText = (element)=>element.type === JsxType.text;
const isJsxValue = (value)=>value?.type === undefined;
const resolveJsxText = (element)=>isJsxValue(element) ? createJsxText(element) : element;
const isBoolean = (value)=>typeof value === "boolean";
const isNull = (value)=>value === null;
const isUndefined = (value)=>typeof value === "undefined";
const isValidJsxValue = (value)=>!isBoolean(value) && !isNull(value) && !isUndefined(value);
const sanitizeJsxElements = (elements)=>elements.flatMap(replaceJsxFragment).filter(isValidJsxValue).map(resolveJsxText);
const createJsxElement = (name, props, children)=>Object.freeze({
        props,
        name,
        type: JsxType.element,
        children: sanitizeJsxElements(children)
    });
const validateJsxElement = (elem)=>{
    const errors = [];
    if (typeof elem !== "object") errors.push(`Jsx element should be an object.`);
    if (!elem?.name) errors.push(`Jsx element should have name. \n${JSON.stringify(elem)}`);
    if (elem?.type !== JsxType.element) errors.push(`Jsx element should be jsx element type. \n${JSON.stringify(elem)}`);
    if (errors.length) throw new Error(errors.join("\n"));
    return elem;
};
const isJsxElementTag = (name)=>typeof name === "string";
const buildJsxFactory = (factory, elem)=>{
    const { props , children , build  } = factory;
    const innerElems = build(Object.freeze(props), children, elem);
    if (innerElems instanceof Array) return sanitizeJsxElements(innerElems);
    if (innerElems instanceof Promise) return [];
    return sanitizeJsxElements([
        innerElems
    ]);
};
const createJsxFactory = (factory, props, children, name)=>Object.freeze({
        build: factory,
        props: props || {},
        name: name || factory.name,
        type: JsxType.factory,
        children: sanitizeJsxElements(children)
    });
const getStoredFactory = (store)=>store.__factory;
const storeFactory = (store, factory)=>store.__factory = factory;
const unstoreFactory = (store)=>delete store.__factory;
const validateJsxFactory = (factory)=>{
    if (typeof factory !== "object") throw new Error(`Jsx factory should be an object.`);
    if (!factory?.name) throw new Error(`Jsx factory should have name.`);
    if (factory?.type !== JsxType.factory) throw new Error(`Jsx factory should be jsx factory type.`);
    if (typeof factory?.build !== "function") throw new Error(`Jsx factory should have build function.`);
    return factory;
};
const getJsxText = (elem)=>isJsxText(elem) && elem.text;
const isJsxFactory = (elem)=>elem.type === JsxType.factory;
const isJsxFactoryTag = (factory)=>typeof factory === "function";
const getJsxFragment = (children)=>Object.freeze({
        name: "Fragment",
        type: JsxType.fragment,
        children: sanitizeJsxElements(children)
    });
const CompileType = Object.freeze({
    element: 0,
    fragment: 1,
    factory: 2,
    unknown: 3
});
const getJsxCompileType = (factoryOrName)=>{
    if (isJsxFactoryTag(factoryOrName)) return CompileType.factory;
    if (isJsxFragmentTag(factoryOrName)) return CompileType.fragment;
    if (isJsxElementTag(factoryOrName)) return CompileType.element;
    return CompileType.unknown;
};
const compileJsxExpression = (factoryOrName, props, ...children)=>{
    const compileType = getJsxCompileType(factoryOrName);
    switch(compileType){
        case CompileType.factory:
            return createJsxFactory(factoryOrName, props, children);
        case CompileType.fragment:
            return getJsxFragment(children);
        case CompileType.element:
            return createJsxElement(factoryOrName, props, children);
        default:
            throw new Error(`Unsupported compile type ${compileType}`);
    }
};
const registerJsxCompiler = (global = globalThis)=>global.React = {
        createElement: compileJsxExpression
    };
const isEmptyArray = (arr)=>arr.length === 0;
const isLengthArraysEquality = (arr1, arr2)=>arr1.length === arr2.length;
const isArrayType = (val)=>val instanceof Array;
const isObjectType = (val)=>typeof val === "object";
const isArraysEquality = (arr1, arr2)=>isEmptyArray(arr1) && isEmptyArray(arr2) || isLengthArraysEquality(arr1, arr2) && isObjectsEquality(arr1, arr2);
const isObjectsEquality = (obj1, obj2)=>JSON.stringify(obj1) === JSON.stringify(obj2);
const EqualityType = Object.freeze({
    arrays: 0,
    objects: 1,
    nonObjects: 2
});
const getEqualityType = (val1, val2)=>{
    if (isArrayType(val1) && isArrayType(val2)) return EqualityType.arrays;
    if (isObjectType(val1) && isObjectType(val2)) return EqualityType.objects;
    return EqualityType.nonObjects;
};
const isValuesEquality = (val1, val2)=>{
    const equalityType = getEqualityType(val1, val2);
    switch(equalityType){
        case EqualityType.objects:
            return isObjectsEquality(val1, val2);
        case EqualityType.arrays:
            return isArraysEquality(val1, val2);
        case EqualityType.nonObjects:
            return val1 === val2;
    }
};
const isDepsEquality = (deps, oldDeps)=>deps !== undefined ? isValuesEquality(deps, oldDeps) : false;
const deferEffect = (func)=>async (phaseType)=>await func(phaseType);
const createEffect = (base, finalize)=>({
        base: deferEffect(base),
        finalize
    });
const getHookName = (hooks)=>hooks.offset === undefined ? hooks.offset = 0 : ++hooks.offset;
const getHookValue = (hooks, name)=>hooks[name]?.value;
const createHook = (value, deps, hookType)=>Object.freeze({
        value,
        deps,
        hookType
    });
const getCurrentHooks = ()=>globalThis.__hooks;
const getStoredHooks = (store)=>store.__hooks;
const getStoredHook = (hooks, name)=>hooks[name];
const storeCurrentHooks = (hooks)=>globalThis.__hooks = hooks;
const storeHook = (hooks, name, hook)=>hooks[name] = hook;
const storeHooks = (store, hooks)=>store.__hooks = hooks;
const validateFunc = (func, name)=>{
    if (typeof func !== "function") throw new Error(`${name} param not function`);
};
const validateDeps = (deps, name)=>{
    if (!(deps ?? [] instanceof Array)) throw new Error(`${name} optional param not array`);
};
const HookType = Object.freeze({
    state: 0,
    callback: 1,
    context: 2,
    memo: 3,
    effect: 4,
    layoutEffect: 5,
    reducer: 6
});
const useEffect = (func, deps, hooks = getCurrentHooks())=>{
    validateFunc(func, "effect func");
    validateDeps(deps, "effect deps");
    const name = getHookName(hooks);
    const hook = getStoredHook(hooks, name);
    if (!hook || !isDepsEquality(deps, hook.deps)) {
        const prevEffect = getHookValue(hooks, name);
        const effect = createEffect(func, prevEffect?.finalize);
        const hook1 = createHook(effect, deps, HookType.effect);
        storeHook(hooks, name, hook1);
    }
    return getHookValue(hooks, name);
};
const getLayoutEffect = (base, finalize)=>({
        base,
        finalize
    });
const useLayoutEffect = (func, deps, hooks = getCurrentHooks())=>{
    validateFunc(func, "layout effect func");
    validateDeps(deps, "layout effect deps");
    const name = getHookName(hooks);
    const hook = getStoredHook(hooks, name);
    if (!hook || !isDepsEquality(deps, hook.deps)) {
        const prevEffect = getHookValue(hooks, name);
        const effect = getLayoutEffect(func, prevEffect?.finalize);
        const hook1 = createHook(effect, deps, HookType.layoutEffect);
        storeHook(hooks, name, hook1);
    }
    return getHookValue(hooks, name);
};
const useCallback = (func, deps, hooks = getCurrentHooks())=>{
    validateFunc(func, "callback func");
    validateDeps(deps, "callback deps");
    const name = getHookName(hooks);
    const hook = getStoredHook(hooks, name);
    if (!hook || !isDepsEquality(deps, hook.deps)) {
        const hook1 = createHook(func, deps, HookType.callback);
        storeHook(hooks, name, hook1);
    }
    return getHookValue(hooks, name);
};
const useMemo = (func, deps, hooks = getCurrentHooks())=>{
    validateFunc(func, "memo func");
    validateDeps(deps, "memo deps");
    const name = getHookName(hooks);
    const Hook = getStoredHook(hooks, name);
    if (!Hook || !isDepsEquality(deps, Hook.deps)) {
        const Hook1 = createHook(func(), deps, HookType.memo);
        storeHook(hooks, name, Hook1);
    }
    return getHookValue(hooks, name);
};
const getReducerHookValue = (state, reducer)=>Object.freeze({
        state,
        reducer
    });
const dispatchAction = (hooks, reducer, name)=>(action)=>{
        const value = getHookValue(hooks, name);
        const state = reducer(value.state, action);
        const newValue = getReducerHookValue(state, reducer);
        const hook = createHook(newValue, undefined, HookType.reducer);
        storeHook(hooks, name, hook);
    };
const useReducer = (reducer, initState, init, hooks = getCurrentHooks())=>{
    const name = getHookName(hooks);
    const hook = getStoredHook(hooks, name);
    if (!hook) {
        const state = init ? init(initState) : initState;
        const value = getReducerHookValue(state, reducer);
        const hook1 = createHook(value, undefined, HookType.reducer);
        storeHook(hooks, name, hook1);
    }
    return [
        getHookValue(hooks, name).state,
        dispatchAction(hooks, reducer, name)
    ];
};
const resolveHookValue = (valueOrFunc)=>typeof valueOrFunc === "function" ? valueOrFunc() : valueOrFunc;
const useState = (valueOrFunc, hooks = getCurrentHooks())=>{
    const name = getHookName(hooks);
    const hook = getStoredHook(hooks, name);
    if (!hook) {
        const value = resolveHookValue(valueOrFunc);
        const hook1 = createHook(value, undefined, HookType.state);
        storeHook(hooks, name, hook1);
    }
    return [
        getHookValue(hooks, name),
        (value)=>{
            const hook = createHook(value, undefined, HookType.state);
            storeHook(hooks, name, hook);
        }
    ];
};
const useStates = (states, hooks = getCurrentHooks())=>hooks.states ? [
        hooks.states,
        ()=>hooks.states
    ] : [
        hooks.states = states,
        ()=>hooks.states
    ];
export { useEffect as useEffect };
export { useLayoutEffect as useLayoutEffect };
export { useCallback as useCallback };
export { useMemo as useMemo };
export { useReducer as useReducer };
export { useState as useState, useStates as useStates };
const getCurrentElement = ()=>globalThis.__elem;
const storeCurrentElement = (elem)=>globalThis.__elem = elem;
const nullLogger = Object.freeze({
    info: ()=>{},
    error: ()=>{}
});
const getCurrentLogger = (elem)=>elem.__log instanceof Array && elem.__log.includes("rendering") ? console : nullLogger;
const getTimeNow = ()=>new Date(Date.now()).toISOString();
const logInfo = (elem, ...args)=>getCurrentLogger(elem).info("[rendering]", `[${getTimeNow()}]`, ...args);
const switchLogging = ($elem, $parent)=>$parent?.__log instanceof Array && ($elem.__log = [
        ...$parent.__log
    ]);
const findEffects = (hooks)=>Object.values(hooks).filter((hook)=>hook?.hookType === HookType.effect).map((hook)=>hook.value);
const cleanEffect = (effect)=>{
    effect.base = undefined;
    effect.finalize = undefined;
    return effect;
};
const runEffect = async (effect, phaseType)=>{
    const { base , finalize  } = effect;
    cleanEffect(effect);
    await finalize?.(phaseType);
    const result = await base?.(phaseType);
    if (typeof result === "function") effect.finalize = result;
    return effect;
};
const resetHooks = (hooks)=>(delete hooks.offset) && hooks;
const unstoreHooks = (store)=>delete store.__hooks;
const unstoreCurrentHooks = ()=>delete globalThis.__hooks;
const findLayoutEffects = (hooks)=>Object.values(hooks).filter((hook)=>hook?.hookType === HookType.layoutEffect).map((hook)=>hook.value);
const cleanLayoutEffect = (effect)=>{
    effect.base = undefined;
    effect.finalize = undefined;
    return effect;
};
const runLayoutEffect = (effect, phaseType)=>{
    const { base , finalize  } = effect;
    cleanLayoutEffect(effect);
    finalize?.(phaseType);
    const result = base?.(phaseType);
    if (typeof result === "function") effect.finalize = result;
    return effect;
};
const isKeyEquality = (elem, $elem)=>elem?.props?.key === $elem?.key;
const isNameEquality = (elem, $elem)=>getJsxName(elem) === getHtmlName($elem);
const isPropsEquality = (elem, $elem)=>isValuesEquality(elem.props, getStoredFactory($elem).props);
const isTextEquality = (elem, $elem)=>getJsxText(elem) === getHtmlText($elem);
const BuildTypes = Object.freeze({
    build: 0,
    update: 1,
    destroy: 2,
    skip: 3
});
const getBuildType = (elem, $elem)=>{
    if (!$elem) return BuildTypes.build;
    if (!elem) return BuildTypes.destroy;
    if (!isNameEquality(elem, $elem)) return BuildTypes.build;
    if (!isKeyEquality(elem, $elem)) return BuildTypes.build;
    if (isJsxFactory(elem) && isPropsEquality(elem, $elem)) return BuildTypes.skip;
    return BuildTypes.update;
};
const MountTypes = Object.freeze({
    append: 0,
    replace: 1,
    remove: 2,
    skip: 3
});
const getMountType = (elem, $elem)=>{
    if (!$elem) return MountTypes.append;
    if (!elem) return MountTypes.remove;
    if (!isNameEquality(elem, $elem)) return MountTypes.replace;
    if (!isKeyEquality(elem, $elem)) return MountTypes.replace;
    if (!isTextEquality(elem, $elem)) return MountTypes.replace;
    return MountTypes.skip;
};
const storeInternalData = ($elem, elem)=>{
    storeProps($elem, elem.props);
    if (isJsxFactory(elem)) {
        storeFactory($elem, elem);
        storeHooks($elem, {});
    }
    return $elem;
};
const updateInternalData = ($elem, elem)=>{
    if (isJsxFactory(elem)) storeFactory($elem, elem);
    return $elem;
};
const isInternalDataName = (name)=>name.startsWith("__");
const unstoreInternalData = ($elem)=>{
    unstoreProps($elem);
    unstoreFactory($elem);
    unstoreHooks($elem);
    Object.getOwnPropertyNames($elem).filter(isInternalDataName).forEach((propName)=>delete $elem[propName]);
    return $elem;
};
const createReconciliation = (elem, $elem, ops)=>Object.freeze({
        elem,
        $elem,
        ops
    });
const reconcileElement = (ops, elem, $elem, $parent)=>{
    const $built = function() {
        switch(ops.buildType){
            case BuildTypes.build:
                return !isJsxText(elem) ? buildHtmlElement(elem) : buildHtmlText(elem);
            case BuildTypes.update:
                return !isJsxText(elem) ? updateHtmlElement($elem, elem) : updateHtmlText($elem, elem);
            case BuildTypes.destroy:
                return !isHtmlText($elem) ? destroyHtmlElement($elem, getStoredProps($elem)) : $elem;
            case BuildTypes.skip:
                return $elem;
            default:
                throw new Error(`Unsupported build type ${ops.buildType}.`);
        }
    }();
    const $builtWithInternals = function() {
        switch(ops.buildType){
            case BuildTypes.build:
                return storeInternalData($built, elem);
            case BuildTypes.update:
                return updateInternalData($built, elem);
            case BuildTypes.destroy:
                return unstoreInternalData($built);
            default:
                return $built;
        }
    }();
    const $mounted = function() {
        switch(ops.mountType){
            case MountTypes.append:
                return appendHtmlNode($builtWithInternals, $parent);
            case MountTypes.replace:
                return replaceHtmlNode($builtWithInternals, $elem);
            case MountTypes.remove:
                return getHtmlParentElement($builtWithInternals) ? removeHtmlNode($builtWithInternals) : $builtWithInternals;
            case MountTypes.skip:
                return $builtWithInternals;
            default:
                throw new Error(`Unsupported mounting type ${ops.mountType}.`);
        }
    }();
    return createReconciliation(elem, $mounted, ops);
};
const createOperations = (buildType, mountType)=>Object.freeze({
        buildType,
        mountType
    });
const createDifference = (elem, $elem, ops)=>Object.freeze({
        elem,
        $elem,
        ops
    });
const diffElements = (elems, $elems)=>{
    const diffs = [];
    const maxLength = elems.length > $elems.length ? elems.length : $elems.length;
    const isReplacedElement = ($elem, mountType)=>MountTypes.replace === mountType && !isHtmlText($elem);
    for(let index = 0; index < maxLength; index++){
        const elem = elems[index];
        const $elem = $elems[index];
        const buildType = getBuildType(elem, $elem);
        const mountType = getMountType(elem, $elem);
        const ops = createOperations(buildType, mountType);
        const diff = createDifference(elem, $elem, ops);
        diffs.push(diff);
        if (isReplacedElement($elem, mountType)) {
            const buildType1 = BuildTypes.destroy;
            const mountType1 = MountTypes.remove;
            const ops1 = createOperations(buildType1, mountType1);
            const diff1 = createDifference(elem, $elem, ops1);
            diffs.push(diff1);
        }
    }
    return diffs;
};
const unstoreCurrentElement = ()=>delete globalThis.__elem;
const buildJsxFactoryChildren = (elem, $elem)=>{
    resetHooks(getStoredHooks($elem) ?? {});
    storeCurrentHooks(getStoredHooks($elem));
    storeCurrentElement($elem);
    const children = buildJsxFactory(elem, $elem);
    unstoreCurrentHooks();
    unstoreCurrentElement();
    return children;
};
const buildJsxChildren = (elem, $elem)=>isJsxFactory(elem) ? buildJsxFactoryChildren(elem, $elem) : getJsxChildren(elem);
const isFactoryElement = ($elem)=>getStoredFactory($elem);
const isKeyElement = (elem)=>elem?.props && "key" in elem.props;
const isPortalElement = (elem)=>getHtmlName(elem) === "portal";
const isStyleElement = (elem)=>getHtmlName(elem) === "style";
const RenderPhases = Object.freeze({
    render: 0,
    update: 1,
    unrender: 2,
    skip: 3
});
const getRenderPhaseName = (renderPhase)=>{
    switch(renderPhase){
        case RenderPhases.render:
            return "render";
        case RenderPhases.update:
            return "update";
        case RenderPhases.unrender:
            return "unrender";
        case RenderPhases.skip:
            return "skip";
    }
};
const runLayoutEffects = (elem, renderPhase)=>{
    if (!isFactoryElement(elem)) return;
    storeCurrentElement(elem);
    const hooks = getStoredHooks(elem);
    findLayoutEffects(hooks).forEach((effect, index)=>{
        logInfo(elem, `\trun layout effect [${getRenderPhaseName(renderPhase)} phase] elem: ${getHtmlName(elem)}`, index);
        runLayoutEffect(effect, renderPhase);
    });
    unstoreCurrentElement();
    return elem;
};
const runEffects = (elem, renderPhase)=>{
    if (!isFactoryElement(elem)) return;
    const hooks = getStoredHooks(elem);
    findEffects(hooks).forEach((effect, index)=>{
        logInfo(elem, `\trun effect [${getRenderPhaseName(renderPhase)} phase] elem: ${getHtmlName(elem)}`, index);
        runEffect(effect, renderPhase).then(()=>logInfo(elem, `\tend run effect [${getRenderPhaseName(renderPhase)} phase] elem: ${getHtmlName(elem)}`, index));
    });
    return elem;
};
const runAllEffects = ($elem, renderPhase)=>{
    runLayoutEffects($elem, renderPhase);
    runEffects($elem, renderPhase);
    return $elem;
};
const switchIgnoring = ($elem, $parent)=>$parent?.__ignore instanceof Array && ($elem.__ignore = [
        ...$parent.__ignore
    ]);
const isIgnoredElement = ($elem)=>$elem.__ignore?.includes(getHtmlName($elem));
const isRenderPhase = (ops)=>ops.buildType === BuildTypes.build;
const isSkipPhase = (ops)=>ops.buildType === BuildTypes.skip;
const isUpdatePhase = (ops)=>ops.buildType === BuildTypes.update;
const isUnrenderPhase = (ops)=>ops.buildType === BuildTypes.destroy;
const skipElement = ($elem, ops)=>{
    if (isSkipPhase(ops)) return true;
    if (isStyleElement($elem)) return true;
    if (isIgnoredElement($elem)) return true;
    if (isPortalElement($elem)) {
        isFactoryElement($elem) && buildJsxFactory(getStoredFactory($elem), $elem);
        return true;
    }
    return false;
};
const renderElements = (elem, $parent = createHtmlElement("main"))=>{
    const ops = createOperations(BuildTypes.build, MountTypes.append);
    const recons = [
        reconcileElement(ops, elem, undefined, $parent)
    ];
    for(let index = 0; index < recons.length; index++){
        const { elem: elem1 , $elem , ops: ops1  } = recons[index];
        switchIgnoring($elem, getHtmlParentElement($elem));
        switchLogging($elem, getHtmlParentElement($elem));
        logInfo($elem, `render elem: ${getHtmlName($elem)}`, elem1.props, ops1);
        skipElement($elem, ops1) || buildJsxChildren(elem1, $elem).map((child)=>reconcileElement(ops1, child, undefined, $elem)).forEach((recon)=>recons.push(recon));
    }
    recons.forEach((recon)=>runAllEffects(recon.$elem, RenderPhases.render));
    return recons;
};
const isKeyEquality1 = (elem, $elem)=>elem?.props?.key === $elem?.key;
const findKeyElement = (elem, $elems)=>$elems?.find(($elem)=>isKeyEquality1(elem, $elem));
const orderKeyElements = (elems, $elems, $parent)=>{
    for(let index = 0; index < elems.length; index++){
        const elem = elems[index];
        const $oldElem = getHtmlChildNodes($parent)[index];
        const $elem = findKeyElement(elem, $elems);
        if ($oldElem && $elem != $oldElem) {
            if ($elem) insertHtmlNode($elem, $oldElem);
            if (!$elem) insertHtmlNode(parseHtmlText(""), $oldElem);
        }
    }
    return getHtmlChildNodes($parent);
};
const toRenderPhase = (ops)=>{
    switch(ops.buildType){
        case BuildTypes.build:
            return RenderPhases.render;
        case BuildTypes.update:
            return RenderPhases.update;
        case BuildTypes.destroy:
            return RenderPhases.unrender;
        case BuildTypes.skip:
            return RenderPhases.skip;
    }
};
const toRenderPhaseName = (ops)=>getRenderPhaseName(toRenderPhase(ops));
const updateChildren = (elem, $elem)=>{
    const children = elem ? buildJsxChildren(elem, $elem) : [];
    const $children = getHtmlChildNodes($elem);
    const diffs = isKeyElement(children[0]) ? diffElements(children, orderKeyElements(children, $children, $elem)) : diffElements(children, $children);
    return diffs.map((diff)=>{
        isUnrenderPhase(diff.ops) && runAllEffects(diff.$elem, toRenderPhase(diff.ops));
        return reconcileElement(diff.ops, diff.elem, diff.$elem, $elem);
    });
};
const updateElements = (elem, $elem)=>{
    const ops = createOperations(BuildTypes.update, MountTypes.skip);
    const recons = [
        reconcileElement(ops, elem, $elem)
    ];
    for(let index = 0; index < recons.length; index++){
        const { elem: elem1 , $elem: $elem1 , ops: ops1  } = recons[index];
        logInfo($elem1, `${toRenderPhaseName(ops1)} elem: ${getHtmlName($elem1)}`, elem1?.props, ops1);
        skipElement($elem1, ops1) || updateChildren(elem1, $elem1).forEach((recon)=>recons.push(recon));
    }
    recons.filter((recon)=>isRenderPhase(recon.ops) || isUpdatePhase(recon.ops)).forEach((recon)=>runAllEffects(recon.$elem, toRenderPhase(recon.ops)));
    return recons;
};
const unrenderElements = ($elem)=>{
    runAllEffects($elem, RenderPhases.unrender);
    const ops = createOperations(BuildTypes.destroy, MountTypes.remove);
    const recons = [
        reconcileElement(ops, undefined, $elem)
    ];
    for(let index = 0; index < recons.length; index++){
        const { $elem: $elem1 , ops: ops1  } = recons[index];
        logInfo($elem1, `unrender elem: ${getHtmlName($elem1)}`, ops1);
        skipElement($elem1, ops1) || getHtmlChildNodes($elem1).map(($child)=>runAllEffects($child, RenderPhases.unrender)).map(($child)=>reconcileElement(ops1, undefined, $child)).forEach((recon)=>recons.push(recon));
    }
    return recons;
};
const ErrorBoundary = ({ error , path  }, children)=>error ? React.createElement("error", null, React.createElement("span", {
        class: "path"
    }, `Path: ${path}`), React.createElement("pre", {
        class: "error"
    }, error)) : children;
const getErrorPath = (boundary, issuer)=>{
    const htmlNames = [];
    while(issuer){
        htmlNames.push(getHtmlName(issuer));
        boundary === issuer ? issuer = undefined : issuer = issuer.parentElement;
    }
    return htmlNames.reverse().join("/");
};
const isErrorBoundaryElement = (elem)=>getHtmlName(elem) === "errorboundary";
const update = ($elem, factory = getStoredFactory($elem))=>{
    validateHtmlElement($elem);
    validateJsxFactory(factory);
    try {
        updateElements(factory, $elem);
    } catch (error) {
        handleError(error);
    }
    return $elem;
};
const handleError = (error, elem = getCurrentElement())=>{
    const boundaryElem = findHtmlAscendant(elem, isErrorBoundaryElement);
    if (boundaryElem) {
        const path = getErrorPath(boundaryElem, elem);
        update(boundaryElem, React.createElement(ErrorBoundary, {
            path: path,
            error: error
        }));
        return error;
    }
    throw error;
};
const getContextName = (name)=>`__${name}.context`;
const isContextElement = (name)=>(elem)=>getContextName(name) in elem;
const isContextProvider = (name)=>(elem)=>getHtmlName(elem) === "context" && getContextName(name) in elem;
const useContext = (name, fallbackValue, elem = getCurrentElement())=>{
    validateHtmlElement(elem);
    const contextName = getContextName(name);
    if (!(contextName in elem)) {
        const context = findHtmlAscendant(elem, isContextElement(name));
        const contextValue = context ? context[contextName] : fallbackValue;
        elem[contextName] = contextValue;
    }
    return [
        elem[contextName],
        (value)=>elem[contextName] = value,
        ()=>updateContext(name, elem[contextName], elem)
    ];
};
const updateContext = (name, value, elem)=>{
    validateHtmlElement(elem);
    const provider = findHtmlAscendant(elem, isContextProvider(name));
    const acceptRootConsumer = provider ?? elem;
    const contexts = findHtmlDescendants(acceptRootConsumer, isContextElement(name));
    return contexts.map((elem)=>{
        const setContextValue = useContext(name, null, elem)[1];
        setContextValue(value);
        return update(elem);
    });
};
const Context = (props, children)=>{
    const [, setContext, updateContext] = useContext(props.name, props.value);
    useLayoutEffect((phase)=>{
        if (phase) {
            setContext(props.value);
            updateContext();
        }
    }, [
        props.value
    ]);
    return children;
};
const getStoredLazy = (elem)=>elem.__lazy;
const storeLazy = (elem, func)=>elem.__lazy = func;
const Suspense = ({ suspending =true , fallback  }, children)=>React.createElement(React.Fragment, null, React.createElement("fallback", {
        hidden: !suspending
    }, fallback), React.createElement("section", {
        hidden: suspending
    }, children));
const isSuspenseElement = (elem)=>getHtmlName(elem) === "suspense";
const toggleSuspense = (elem, suspending)=>{
    const suspenseElem = findHtmlAscendant(elem, isSuspenseElement);
    if (suspenseElem) update(suspenseElem, React.createElement(Suspense, {
        suspending: suspending,
        fallback: suspenseElem.fallback
    }));
};
const useSuspense = (elem = getCurrentElement())=>[
        ()=>toggleSuspense(elem, true),
        ()=>toggleSuspense(elem, false)
    ];
const extractLazy = (module, name)=>{
    for(const key in module){
        if (key.toLowerCase() === name.toLowerCase()) return module[key];
    }
};
const validateLazy = (func, name)=>{
    if (typeof func === "function") return func;
    throw new Error(`${name} should be function.`);
};
const lazy = (name, resolver)=>({
        [name]: (props, children, elem)=>{
            validateHtmlElement(elem, "lazy elem");
            validateLazy(resolver, `lazy resolver`);
            const [suspense, unsuspense] = useSuspense();
            const func = getStoredLazy(elem);
            if (func) return func(props, children, elem);
            suspense();
            return resolver().then((module)=>{
                unsuspense();
                const func = extractLazy(module, name);
                validateLazy(func, `module ${name}`);
                storeLazy(elem, func);
                const factory = createJsxFactory(func, props, children, name);
                validateJsxFactory(factory);
                update(elem, factory);
            }).catch((error)=>{
                unsuspense();
                return Promise.reject(error);
            });
        }
    })[name];
const storeFunc = (elem, name, action)=>elem.ownerDocument[`__${name}`] = action;
const unrender = ($elem)=>{
    validateHtmlElement($elem);
    try {
        unrenderElements($elem);
    } catch (error) {
        handleError(error);
    }
    return $elem;
};
const render = (factoryOrElem, $parent = parseRootHtml("<main></main>"))=>{
    validateHtmlElement($parent);
    isJsxFactory(factoryOrElem) ? validateJsxFactory(factoryOrElem) : validateJsxElement(factoryOrElem);
    storeFunc($parent, "render", render);
    storeFunc($parent, "update", update);
    storeFunc($parent, "unrender", unrender);
    try {
        const { $elem  } = renderElements(factoryOrElem, $parent)[0];
        return $elem;
    } catch (error) {
        handleError(error);
        return $parent;
    }
};
const renderElements1 = (elems, $parent)=>elems.flatMap((elem)=>render(elem, $parent));
const renderExternalElements = ($elem, elems)=>$elem.attachShadow ? renderElements1(elems, $elem.attachShadow({
        mode: 'closed'
    })) : renderElements1(elems, $elem);
const Portal = (_, children, elem)=>{
    if (elem.__initialized) return React.createElement(React.Fragment, null);
    elem.__initialized = true;
    renderExternalElements(elem, children);
    return React.createElement(React.Fragment, null);
};
const getStoredServices = (elem)=>elem.ownerDocument.__services;
const storeServices = (elem, services)=>elem.ownerDocument.__services = services;
const Services = (props, children, elem)=>{
    storeServices(elem, props);
    return children;
};
const useService = (name, fallback, elem = getCurrentElement())=>{
    const services = getStoredServices(elem);
    return services?.[name] ? services[name] : fallback;
};
export { Context as Context };
export { useContext as useContext };
export { ErrorBoundary as ErrorBoundary };
export { handleError as handleError };
export { lazy as lazy };
export { Portal as Portal };
export { Services as Services };
export { useService as useService };
export { Suspense as Suspense };
export { useSuspense as useSuspense };
export { render as render };
export { update as update };
export { unrender as unrender };
registerJsxCompiler();
try {
    globalThis["Deno"] && await registerDOMParser();
} catch (error) {
    console.error(error);
}
