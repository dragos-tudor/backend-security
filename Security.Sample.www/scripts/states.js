const ActionType = Symbol("action");
const createAction = (type, payload)=>Object.freeze({
        type,
        payload,
        $type: ActionType
    });
const splitActionType = (type, delimiter = "/")=>type.split(delimiter);
const isActionType = (action)=>action.$type === ActionType;
const isActionTypeFormat = (action)=>typeof action.type === "string" && splitActionType(action.type).length === 2;
const validateActionType = (action)=>isActionType(action) ? "" : "Action type should be action [use createAction].";
const validateActionTypeFormat = (action)=>isActionTypeFormat(action) ? "" : "Action type should have slice/reducer format.";
const validateAction = (action)=>[
        validateActionType(action),
        validateActionTypeFormat(action)
    ].filter((error)=>error);
const throwError = (message)=>{
    if (!message) return false;
    throw new Error(message);
};
const throwErrors = (messages)=>{
    if (!messages.length) return false;
    throw new Error(messages.join(","));
};
const getHtmlBody = (elem)=>elem.ownerDocument.body;
const getHtmlName = (elem)=>elem.tagName?.toLowerCase();
const getHtmlParentElement = (elem)=>elem.parentElement;
const findHtmlAscendant = (elem, func)=>{
    if (func(elem)) return elem;
    if (!getHtmlParentElement(elem)) return undefined;
    return findHtmlAscendant(getHtmlParentElement(elem), func);
};
const findHtmlDescendants = (elem, func, elems = [])=>{
    if (func(elem)) elems.push(elem);
    for(let index = 0; index < elem.children.length; index++)findHtmlDescendants(elem.children[index], func, elems);
    return elems;
};
const findHtmlRoot = (elem)=>globalThis["Deno"] ? findHtmlAscendant(elem, (elem)=>!getHtmlParentElement(elem)) : getHtmlBody(elem);
const isHtmlElement = (elem)=>elem.nodeType === 1;
const validateHtmlElement = (elem)=>isHtmlElement(elem) ? "" : "Element type should be HTML element.";
const isLogLibraryEnabled = (elem, libraryName)=>elem.__log.includes(libraryName);
const isLogMounted = (elem)=>elem.__log instanceof Array;
const isLogEnabled = (elem, libraryName)=>isLogMounted(elem) && isLogLibraryEnabled(elem, libraryName);
const LibraryName = "states";
const LogHeader = "[states]";
const logInfo = (elem, ...args)=>isLogEnabled(elem, LibraryName) && console.info(LogHeader, ...args);
const countObjectProps = (obj)=>Object.getOwnPropertyNames(obj).length;
const ReservedPropNames = Object.freeze([
    "children"
]);
const existsObject = (obj)=>obj != null;
const isObjectType = (value)=>typeof value === "object" && value !== null;
const isValidObjectPropName = (propName)=>!ReservedPropNames.includes(propName);
const getObjectPropNames = (obj)=>Object.getOwnPropertyNames(obj).filter(isValidObjectPropName);
const equalArraysLength = (arr1, arr2)=>arr1.length === arr2.length;
const existsArray = (arr)=>arr != null;
const isArrayType = (value)=>value instanceof Array;
const isFunctionType = (value)=>typeof value === "function";
const equalArrays = (arr1, arr2)=>{
    if (!existsArray(arr1) || !existsArray(arr2)) return arr1 === arr2;
    if (!equalArraysLength(arr1, arr2)) return false;
    for(let index = 0; index < arr1.length; index++)if (!equalValues(arr1[index], arr2[index])) return false;
    return true;
};
const equalValues = (value1, value2)=>{
    if (isFunctionType(value1) && isFunctionType(value2)) return true;
    if (isArrayType(value1) && isArrayType(value2)) return equalArrays(value1, value2);
    if (isObjectType(value1) && isObjectType(value2)) return equalObjects(value1, value2);
    return value1 === value2;
};
const equalObjectsProp = (obj1, obj2, propName)=>equalValues(obj1[propName], obj2[propName]);
const equalObjectsProps = (obj1, obj2)=>getObjectPropNames(obj1).every((propName)=>equalObjectsProp(obj1, obj2, propName));
const equalObjects = (obj1, obj2)=>{
    if (!existsObject(obj1) || !existsObject(obj2)) return obj1 === obj2;
    if (countObjectProps(obj1) !== countObjectProps(obj2)) return false;
    return equalObjectsProps(obj1, obj2);
};
const getSelector = (selectors, name)=>selectors[name];
const getSelectors = (elem)=>elem.__selectors;
const setSelector = (selectors, selector)=>selectors[selector.name] = selector;
const setSelectorValue = (selector, value)=>selector.value = value;
const setSelectors = (elem, selectors = {})=>elem.__selectors = elem.__selectors || selectors;
const createSelector = (name, func, value)=>({
        name,
        func,
        value
    });
const isFunctionSelectorFunc = (func)=>typeof func === "function";
const isStringSelectorName = (name)=>typeof name === "string";
const validateSelectorFunc = (func)=>isFunctionSelectorFunc(func) ? "" : "Selector func should be function.";
const validateSelectorName = (name)=>isStringSelectorName(name) ? "" : "Selector name should be string.";
const useSelector = (selectors, name, func, states)=>{
    throwError(validateSelectorFunc(func));
    throwError(validateSelectorName(name));
    const value = func(states);
    getSelector(selectors, name) ? setSelectorValue(getSelector(selectors, name), value) : setSelector(selectors, createSelector(name, func, value));
    return value;
};
const StateType = Symbol("state");
const createState = (name, data)=>Object.freeze({
        name,
        data,
        $type: StateType
    });
const getState = (states, name)=>states[name];
const getStates = (elem)=>elem.ownerDocument.__states;
const setState = (states, state)=>states[state.name] = state.data;
const setStates = (elem, states = {})=>elem.ownerDocument.__states = states;
const existsState = (states, name)=>!!getState(states, name);
const isStateType = (reducers)=>reducers.$type === StateType;
const validateStateType = (state)=>isStateType(state) ? "" : "State type should be state [use createState].";
const validateState = (state)=>validateStateType(state);
const isStateChanged = (elem)=>(selector)=>!equalValues(selector.value, selector.func(getStates(elem)));
const isConsumer = (elem)=>!!getSelectors(elem);
const isUpdatableConsumer = (elem)=>Object.values(getSelectors(elem)).filter(isStateChanged(elem)).some((elem)=>elem);
const findConsumers = (elem)=>findHtmlDescendants(elem, isConsumer);
const getUpdateFunc = (elem)=>elem?.ownerDocument.__update;
const updateConsumer = (update)=>(elem)=>(logInfo(elem, "update global states consumer:", getHtmlName(elem)), update(elem)[0]);
const updateConsumers = (elem, update = getUpdateFunc(elem))=>findConsumers(elem).filter(isUpdatableConsumer).map(updateConsumer(update));
const MiddlewareType = Symbol("middleware");
const createMiddleware = (name, func)=>Object.freeze({
        name,
        func,
        $type: MiddlewareType
    });
const chainMiddlewares = (middlewares, lastMiddleware)=>middlewares.map((middleware)=>middleware.func).reverse().reduce((chain, func)=>func(chain), lastMiddleware);
const getMiddleware = (middlewares, name)=>middlewares.find((middleware)=>middleware.name === name);
const getMiddlewares = (elem)=>elem.ownerDocument.__middlewares;
const setMiddleware = (middlewares, middleware)=>(middlewares.push(middleware), middleware);
const setMiddlewares = (elem, middlewares = [])=>elem.ownerDocument.__middlewares = middlewares;
const existsMiddleware = (middlewares, name)=>!!getMiddleware(middlewares, name);
const isFunctionMiddlewareFunc = (middleware)=>typeof middleware.func === "function";
const isMiddlewareType = (middleware)=>middleware.$type === MiddlewareType;
const isStringMiddlewareName = (middleware)=>typeof middleware.name === "string";
const validateMiddlewareFunc = (middleware)=>isFunctionMiddlewareFunc(middleware) ? "" : "Middleware func should be function.";
const validateMiddlewareName = (middleware)=>isStringMiddlewareName(middleware) ? "" : "Middleware name should be string.";
const validateMiddlewareType = (middleware)=>isMiddlewareType(middleware) ? "" : "Middleware type should be middleware [use createMiddleware].";
const validateMiddleware = (middleware)=>[
        validateMiddlewareType(middleware),
        validateMiddlewareName(middleware),
        validateMiddlewareFunc(middleware)
    ].filter((error)=>error);
const ReducerType = Symbol("reducer");
const createReducer = (name, funcs)=>Object.freeze({
        name,
        funcs,
        $type: ReducerType
    });
const getReducer = (elem, name)=>elem[name];
const getReducers = (elem)=>elem.ownerDocument.__reducers;
const setReducer = (reducers, reducer)=>reducers[reducer.name] = reducer.funcs;
const setReducers = (elem, reducers = {})=>elem.ownerDocument.__reducers = reducers;
const existsReducer = (reducers, name)=>!!getReducer(reducers, name);
const isFunctionsReducerFuncs = (reducers)=>Object.values(reducers.funcs ?? {}).every((func)=>typeof func === "function");
const isReducerType = (reducers)=>reducers.$type === ReducerType;
const isStringReducerName = (reducers)=>typeof reducers.name === "string";
const validateReducerFuncs = (reducer)=>isFunctionsReducerFuncs(reducer) ? "" : "Reducer funcs should contains functions.";
const validateReducerName = (reducer)=>isStringReducerName(reducer) ? "" : "Reducer name should be string.";
const validateReducerType = (reducer)=>isReducerType(reducer) ? "" : "Reducer type should be reducer [use createReducer].";
const validateReducer = (reducer)=>[
        validateReducerType(reducer),
        validateReducerName(reducer),
        validateReducerFuncs(reducer)
    ].filter((error)=>error);
const runAction = (action, reducers, states)=>{
    const [stateName, reducerFuncName] = splitActionType(action.type);
    const state = states?.[stateName];
    const reducer = reducers?.[stateName];
    if (!reducer || !reducer[reducerFuncName]) return states;
    const reducerFunc = reducer[reducerFuncName];
    const reducedState = reducerFunc(state, action);
    return state == reducedState ? states : {
        ...states,
        [stateName]: reducedState
    };
};
const dispatchAction = (elem, action)=>{
    throwError(validateHtmlElement(elem));
    throwErrors(validateAction(action));
    logInfo(elem, "start dispatch action", action, "elem", getHtmlName(elem));
    const middlewares = getMiddlewares(elem);
    if (middlewares) {
        const middlewaresChain = chainMiddlewares(middlewares, (action)=>action);
        middlewaresChain(action);
    }
    const states = getStates(elem);
    const reducers = getReducers(elem);
    const reducedStates = runAction(action, reducers, states);
    if (states === reducedStates) return [];
    setStates(elem, reducedStates);
    logInfo(elem, "end dispatch action", action, "states", states, "reduces states", reducedStates);
    const root = findHtmlRoot(elem);
    return updateConsumers(root);
};
export { dispatchAction as dispatchAction };
const Store = (props, elem)=>{
    const { reducer, state, middleware } = props;
    const middlewares = getMiddlewares(elem) || setMiddlewares(elem);
    const reducers = getReducers(elem) || setReducers(elem);
    const states = getStates(elem) || setStates(elem);
    if (reducer) {
        throwErrors(validateReducer(reducer));
        existsReducer(reducers, reducer.name) || setReducer(reducers, reducer);
    }
    if (state) {
        throwErrors(validateState(state));
        existsState(states, state.name) || setState(states, state);
    }
    if (middleware) {
        throwErrors(validateMiddleware(middleware));
        existsMiddleware(middlewares, middleware.name) || setMiddleware(middlewares, middleware);
    }
    return props.children;
};
export { Store as Store };
export { createAction as createAction };
export { createMiddleware as createMiddleware };
export { createReducer as createReducer };
export { setSelectors as setSelectors };
export { useSelector as useSelector };
export { createState as createStoreState };
export { getStates as getStoreStates };
