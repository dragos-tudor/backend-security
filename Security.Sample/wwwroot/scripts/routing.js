// deno-fmt-ignore-file
// deno-lint-ignore-file
// This code was bundled using `deno bundle` and it's not recommended to edit it manually

const getCurrentElement = (global = globalThis)=>global.__elem;
const getHtmlName = (elem)=>elem.tagName.toLowerCase();
const findHtmlAscendant = (elem, func)=>{
    while(elem){
        if (func(elem)) return elem;
        elem = elem.parentElement;
    }
};
const findHtmlDescendant = (elem, func = (e)=>e)=>{
    const elems = [
        elem
    ];
    for (const elem1 of elems){
        if (func(elem1)) return elem1;
        elems.push(...Array.from(elem1.children));
    }
};
const isHtmlElement = (elem)=>elem?.constructor?.name.endsWith("Element");
const validateHtmlElement = (elem, name)=>{
    if (isHtmlElement(elem)) return elem;
    throw new Error(`'${name}' should be HTML Element.`);
};
const storeEventHandler = (elem, handlerName, handler)=>elem[handlerName] = handler;
const listenForEvent = (elem, eventName, handler)=>{
    const handlerName = `on${eventName}`;
    elem.removeEventListener(eventName, elem[handlerName]);
    storeEventHandler(elem, handlerName, handler);
    elem.addEventListener(eventName, handler);
    return elem;
};
const getLastHistoryHref = (history)=>history.hrefs[history.hrefs.length - 1];
const getHistoryPolyfill = (hrefs = [])=>Object.freeze({
        hrefs,
        pushState: (_, __, href)=>hrefs.push(href)
    });
const getStoredHistory = (elem)=>elem.ownerDocument.__history;
const storeCurrentHistory = (elem, history)=>elem.__history = getLastHistoryHref(history);
const storeHistory = (elem)=>globalThis.history ? elem.ownerDocument.__history = globalThis.history : elem.ownerDocument.__history = getHistoryPolyfill();
const isCurrentHistoryEquality = (elem, history)=>elem.__history ? elem.__history === getLastHistoryHref(history) : true;
const changeLocation = (history, url, state = {})=>history.pushState(state, "", url);
const getLocationUrl = (url)=>url.startsWith("/") ? url : "/" + url;
const getStoredLocation = (elem)=>elem.ownerDocument.__location;
const storeCurrentLocation = (elem, location)=>elem.__location = location.href;
const storeLocation = (elem, url)=>globalThis["Deno"] ? elem.ownerDocument.__location = new URL(`http://test.com${getLocationUrl(url)}`) : elem.ownerDocument.__location = globalThis.location;
const unstoreLocation = (elem)=>elem.ownerDocument.__location = undefined;
const isCurrentLocationEquality = (elem, location)=>elem.__location ? elem.__location === location.href : true;
const isHtmlNavLink = (elem)=>getHtmlName(elem) === "navlink";
const isHtmlRouter = (elem)=>getHtmlName(elem) === "router";
const findAscendantRouter = (elem)=>findHtmlAscendant(elem, isHtmlRouter);
const listenForHistory = (elem, navigate)=>listenForEvent(elem, "popstate", navigate);
const listenForNavigation = (elem, navigate)=>listenForEvent(elem, "click", (event)=>{
        const navlink = findHtmlAscendant(event.target, isHtmlNavLink);
        if (!navlink) return;
        event.preventDefault();
        navigate({
            ...event,
            target: navlink.children[0]
        });
    });
const getStoredRouter = (elem)=>elem.ownerDocument.__router;
const storeRouter = (elem, router)=>elem.ownerDocument.__router = router;
const getStoredRenderFunc = (elem)=>elem?.ownerDocument?.__render;
const getStoredUpdateFunc = (elem)=>elem?.ownerDocument?.__update;
const validateFunc = (func, name)=>{
    if (typeof func !== "function") throw new Error(`${name} should be function.`);
    return func;
};
const getStringType = (value)=>{
    if (value.startsWith("{") && value.endsWith("}")) return "object";
    if (value.startsWith("[") && value.endsWith("]")) return "array";
    if (!isNaN(value) && !isNaN(parseFloat(value))) return "float";
    if (!isNaN(value) && !isNaN(parseInt(value))) return "integer";
    if (!isNaN(Date.parse(value))) return "date";
};
const convertValueType = (value)=>{
    if (typeof value !== 'string') return value;
    switch(value){
        case "":
            return "";
        case "true":
            return true;
        case "false":
            return false;
        case "null":
            return null;
        case "undefined":
            return undefined;
    }
    switch(getStringType(value)){
        case "object":
            return JSON.parse(value);
        case "array":
            return JSON.parse(value);
        case "float":
            return parseFloat(value);
        case "integer":
            return parseInt(value);
        case "date":
            return new Date(Date.parse(value));
        default:
            return value;
    }
};
const getParamName = (path)=>path.replace(":", "");
const includeParam = (path)=>path.startsWith(":") || path.includes("/:");
const isCurrentParamsEquality = (elem, params)=>elem.__params ? elem.__params === JSON.stringify(params) : true;
const extractParams = (url, path)=>{
    if (!path) return undefined;
    const urlParts = url.split("/");
    const routeParts = path.split("/");
    const params = {};
    for(let index = 0; index < routeParts.length; index++)if (includeParam(routeParts[index])) {
        const paramName = getParamName(routeParts[index]);
        const paramValue = convertValueType(urlParts[index]);
        params[paramName] = paramValue;
    }
    return params;
};
const getStoredParams = (elem)=>elem.ownerDocument.__params;
const storeCurrentParams = (elem, params)=>elem.__params = JSON.stringify(params || {});
const storeParams = (elem, params)=>elem.ownerDocument.__params ? Object.assign(elem.ownerDocument.__params, params) : elem.ownerDocument.__params = params;
const unstoreParams = (elem)=>elem.ownerDocument.__params = undefined;
const getDefaultLocation = (global = globalThis)=>global["Deno"] || global.location;
const changeSearchParams = (params, location = getDefaultLocation())=>{
    if (!location) return;
    const searchParams = location.searchParams;
    Object.keys(params).forEach((param)=>searchParams.set(param, encodeURIComponent(params[param])));
    return location;
};
const getFieldName = (field)=>field.split("=")[0];
const getFieldValue = (field)=>convertValueType(field.split("=")[1]);
const extractSearchParams = (url)=>{
    if (!url.includes("?")) return;
    if (url.endsWith("?")) return;
    const rawQueryString = url.split("?")[1];
    const queryString = decodeURIComponent(rawQueryString);
    return queryString.split("&").reduce((params, field)=>{
        params[getFieldName(field)] = getFieldValue(field);
        return params;
    }, {});
};
const removeSearchParams = (url)=>url.split("?")[0];
const isCurrentSearchParamsEquality = (elem, params)=>elem.__searchParams ? elem.__searchParams === JSON.stringify(params) : true;
const getStoredSearchParams = (elem)=>elem.ownerDocument.__searchParams;
const storeCurrentSearchParams = (elem, params)=>elem.__searchParams = JSON.stringify(params || {});
const storeSearchParams = (elem, params)=>elem.ownerDocument.__searchParams = params;
const unstoreSearchParams = (elem)=>elem.ownerDocument.__searchParams = undefined;
const extractUrlRoutePath = (url, route)=>{
    if (route.path === "/") return route.path;
    return url.split("/").filter((_, index)=>index <= route.delimiters).join("/");
};
const getMoreSpecificPathRoute = (newRoute, oldRoute)=>{
    if (!oldRoute) return newRoute;
    if (includeParam(newRoute.path)) return oldRoute;
    if (includeParam(oldRoute.path)) return newRoute;
    return newRoute;
};
const isUrlRoutePartEquality = (urlPart, routePart)=>includeParam(routePart) ? urlPart : urlPart === routePart;
const isUrlRoutePartsEquality = (urlParts, routeParts)=>routeParts.every((routePart, index)=>isUrlRoutePartEquality(urlParts[index], routePart));
const parseUrlForRoute = (url, routes)=>{
    const rootPath = "/";
    const rootRoute = routes.find((route)=>route.path === rootPath);
    let result = rootRoute || url.startsWith(rootPath) ? rootRoute : undefined;
    for (const route of routes){
        const routeParts = route.path.split("/");
        const urlParts = url.split("/");
        if (isUrlRoutePartsEquality(urlParts, routeParts)) result = getMoreSpecificPathRoute(route, result);
    }
    return result;
};
const removeUrlPart = (url, urlPart)=>{
    const remainingUrl = url.replace(urlPart, "");
    return remainingUrl.startsWith("/") ? remainingUrl.substring(1) : remainingUrl;
};
const isUpdatableSubscriber = (elem)=>!isCurrentHistoryEquality(elem, getStoredHistory(elem)) || !isCurrentLocationEquality(elem, getStoredLocation(elem)) || !isCurrentParamsEquality(elem, getStoredParams(elem)) || !isCurrentSearchParamsEquality(elem, getStoredSearchParams(elem));
const updateSubscribers = (subscribers, update)=>subscribers.filter(isUpdatableSubscriber).map((e)=>update(e));
const getStoredSubscribers = (elem)=>elem.ownerDocument.__routing_subscribers;
const storeSubscriber = (elem, subscriber)=>elem.ownerDocument.__routing_subscribers ? elem.ownerDocument.__routing_subscribers.push(subscriber) : elem.ownerDocument.__routing_subscribers = [
        subscriber
    ];
const storeSubscribers = (elem, subscribers)=>elem.ownerDocument.__routing_subscribers = subscribers;
const isSubscribed = (elem)=>elem.__routing_subscriber;
const isValidSubscriber = (elem)=>elem.parentElement;
const refreshSubscribers = (elem)=>{
    const subscribers = getStoredSubscribers(elem) || [];
    const validSubscribers = subscribers.filter(isValidSubscriber);
    return storeSubscribers(elem, validSubscribers);
};
const nullLogger = Object.freeze({
    info: ()=>{},
    error: ()=>{}
});
const getCurrentLogger = (elem)=>elem.__log instanceof Array && elem.__log.includes("routing") ? console : nullLogger;
const getTimeNow = ()=>new Date(Date.now()).toISOString();
const logError = (elem, ...args)=>getCurrentLogger(elem).error(`[routing]`, `[${getTimeNow()}]`, "ERROR", ...args);
const logInfo = (elem, ...args)=>getCurrentLogger(elem).info("[routing]", `[${getTimeNow()}]`, ...args);
const getRouteName = (route)=>typeof route.element === "string" ? route.element.toLowerCase() : route.element.name.toLowerCase();
const createRoute = (props, children = [])=>Object.freeze({
        ...props,
        children,
        delimiters: props.path.split("/").length - 1
    });
const createRouting = (elem, routes, url)=>Object.freeze({
        elem,
        routes,
        url
    });
const isDistinctRoute = (routes, route)=>routes.every((r)=>r.path !== route.path);
const isHtmlRoute = (elem)=>getHtmlName(elem) === "route";
const isMatchingRoute = (url, route)=>url.match(route.path)?.length;
const isRootRoute = (route)=>route.path === "/";
const isImportRoute = (route)=>typeof route.element === "string";
const findFallbackRoute = (routes, url)=>routes.filter((route)=>!isRootRoute(route)).find((route)=>isMatchingRoute(url, route));
const findIndexRoute = (routes)=>routes.find((route)=>route.index);
const findParentRoute = (elem)=>isHtmlRoute(elem.parentElement) ? elem.parentElement : undefined;
const importJsxElement = async (src, name)=>(await import(src))[name];
const createImportJsxElement = async (src, name)=>React.createElement(await importJsxElement(src, name));
const isJsxFactory = (factory)=>{
    if (typeof factory !== "object") return false;
    if (typeof factory.name !== "string") return false;
    if (typeof factory.type !== "number") return false;
    if (typeof factory.build !== "function") return false;
    return true;
};
const validateJsxFactory = (factory, name)=>{
    if (!isJsxFactory(factory)) throw new Error(`${name} should be jsx factory.`);
    return factory;
};
const renderRoute = async (elem, route, render)=>isImportRoute(route) ? render(await createImportJsxElement(route.src, route.element), elem) : render(validateJsxFactory(route.element, "route element"), elem);
const getStoredRoute = (elem)=>elem.__route;
const storeRoute = (elem, route)=>elem.__route = route;
const storeRouteToChildren = (elem, route)=>{
    if (isDistinctRoute(elem.__route.children, route)) elem.__route.children.push(route);
    return elem.__route.children;
};
const isStringPropsElement = (props)=>typeof props.element === "string";
const isStringPropsSrc = (props)=>typeof props.src === "string";
const isValidPropsPath = (props)=>!!props.path;
const isValidPropsElement = (props)=>isJsxFactory(props.element) || isStringPropsElement(props);
const isValidPropsSrc = (props)=>isJsxFactory(props.element) || isStringPropsSrc(props) && isStringPropsElement(props);
const validateRouteProps = (props)=>{
    const errors = [];
    if (!isValidPropsPath(props)) errors.push("Route path prop should not be empty");
    if (!isValidPropsElement(props)) errors.push("Route element prop should be jsx factory or element name");
    if (!isValidPropsSrc(props)) errors.push("Route src and element props should be strings");
    if (errors.length) throw new Error(errors.join("\n"));
    return props;
};
const isHtmlOutlet = (elem)=>getHtmlName(elem) === "outlet";
const findAscendantOutlet = (elem)=>findHtmlAscendant(elem, isHtmlOutlet);
const findDescendantOutlet = (elem)=>findHtmlDescendant(elem, isHtmlOutlet);
const findElement = (elem, route)=>Array.from(elem.children).find((elem)=>getHtmlName(elem) === getRouteName(route));
const displayElement = (elem)=>elem.removeAttribute("hidden");
const hideElement = (elem)=>elem.setAttribute("hidden", true);
const toggleRoutes = (parent, elem)=>Array.from(parent.children).map((child)=>child === elem ? displayElement(child) : hideElement(child));
const switchToRoute = async (elem, route, render, params = {})=>{
    storeRoute(elem, route);
    storeParams(elem, params);
    const oldElem = findElement(elem, route);
    const routeElem = oldElem || await renderRoute(elem, route, render);
    toggleRoutes(elem, routeElem);
    return routeElem;
};
const routeToRoute = async (elem, routes, url, render)=>{
    const route = parseUrlForRoute(url, routes) || findIndexRoute(routes);
    const outlet = findDescendantOutlet(elem);
    if (!route && !url) return;
    if (!route) return new Error(`route not found for url: '${url}'`);
    if (!outlet) return new Error(`outlet not found for route: '${route.path}', url '${url}'`);
    const routeUrl = extractUrlRoutePath(url, route);
    const nextUrl = removeUrlPart(url, routeUrl);
    const routeParams = extractParams(url, route.path);
    const routeElem = await switchToRoute(outlet, route, render, routeParams);
    logInfo(routeElem, `route elem: ${getHtmlName(routeElem)}, route path: ${route.path}, url: ${url}`);
    return createRouting(routeElem, route.children, nextUrl);
};
const fallbackToRoute = async (elem, routes, url, render)=>{
    const route = findFallbackRoute(routes, url);
    const outlet = findDescendantOutlet(elem);
    if (!route) return new Error(`fallback route not found for url: '${url}'`);
    if (!outlet) return new Error(`fallback outlet not found for route: '${route.path}', url: '${url}'`);
    const routeElem = await switchToRoute(outlet, route, render);
    logInfo(routeElem, `fallback route elem: ${getHtmlName(routeElem)}, route path: ${route.path}`);
    return createRouting(routeElem, routes, url);
};
const navigateToUrl = async (elem, url, render)=>{
    logInfo(elem, `navigate to url: ${url}`);
    unstoreLocation(elem);
    unstoreParams(elem);
    unstoreSearchParams(elem);
    storeLocation(elem, url);
    storeSearchParams(elem, extractSearchParams(url));
    const urlWithoutParam = removeSearchParams(url);
    const routes = getStoredRoute(elem).children;
    const routings = [
        await routeToRoute(elem, routes, urlWithoutParam, render)
    ];
    while(true){
        const routing = routings[routings.length - 1];
        if (!routing) return routings;
        if (routing instanceof Error) {
            logError(elem, routing.message);
            const fallbackRouting = await fallbackToRoute(elem, routes, url, render);
            if (fallbackRouting instanceof Error) throw fallbackRouting;
            return routings;
        }
        const nextRouting = await routeToRoute(routing.elem, routing.routes, routing.url, render);
        routings.push(nextRouting);
    }
};
const useNavigate = (elem = getCurrentElement(), render = getStoredRenderFunc(elem), update = getStoredUpdateFunc(elem))=>async (url, fromHistory = false)=>{
        validateHtmlElement(elem, "elem [use navigate]");
        validateHtmlElement(getStoredRouter(elem), "router elem [use navigate]");
        validateFunc(render, "render [use navigate]");
        validateFunc(update, "update [use navigate]");
        if (!fromHistory) changeLocation(getStoredHistory(elem), url);
        const router = getStoredRouter(elem);
        await navigateToUrl(router, url, render);
        const subscribers = refreshSubscribers(router);
        return updateSubscribers(subscribers, update);
    };
const tagAsSubscribed = (elem)=>elem.__routing_subscriber = true;
const useHistory = (elem = getCurrentElement())=>{
    validateHtmlElement(elem, "elem [use history]");
    if (!isSubscribed(elem)) {
        storeSubscriber(elem, elem);
        tagAsSubscribed(elem);
    }
    storeCurrentHistory(elem, getStoredHistory(elem));
    return getStoredHistory(elem);
};
const useLocation = (elem = getCurrentElement())=>{
    validateHtmlElement(elem, "elem [use location]");
    if (!isSubscribed(elem)) {
        storeSubscriber(elem, elem);
        tagAsSubscribed(elem);
    }
    storeCurrentLocation(elem, getStoredLocation(elem));
    return getStoredLocation(elem);
};
const useParams = (elem = getCurrentElement())=>{
    validateHtmlElement(elem, "elem [use params]");
    if (!isSubscribed(elem)) {
        storeSubscriber(elem, elem);
        tagAsSubscribed(elem);
    }
    storeCurrentParams(elem, getStoredParams(elem));
    return getStoredParams(elem);
};
const useSearchParams = (elem = getCurrentElement())=>{
    validateHtmlElement(elem, "elem [use search params]");
    if (!isSubscribed(elem)) {
        storeSubscriber(elem, elem);
        tagAsSubscribed(elem);
    }
    storeCurrentSearchParams(elem, getStoredSearchParams(elem));
    return [
        getStoredSearchParams(elem),
        changeSearchParams
    ];
};
export { useNavigate as useNavigate };
export { useHistory as useHistory };
export { useLocation as useLocation };
export { useParams as useParams };
export { useSearchParams as useSearchParams };
const NavLink = (props, children)=>React.createElement("a", {
        href: props.to
    }, children);
const Router = (_, children, elem)=>{
    storeRoute(elem, {
        children: []
    });
    storeRouter(elem, elem);
    storeHistory(elem);
    const navigate = useNavigate();
    listenForNavigation(elem, (event)=>navigate(event.target.href));
    listenForHistory(window, (event)=>navigate(event.target.location.href, true));
    return children;
};
const Route = (props, children, elem)=>{
    validateRouteProps(props, children);
    const route = createRoute(props);
    storeRoute(elem, route);
    const routeAncestor = findParentRoute(elem) || findAscendantOutlet(elem) || findAscendantRouter(elem);
    if (routeAncestor) storeRouteToChildren(routeAncestor, route);
    return children;
};
const Outlet = (_, children, elem)=>{
    storeRoute(elem, {
        children: []
    });
    return children;
};
export { NavLink as NavLink };
export { Router as Router };
export { Route as Route };
export { Outlet as Outlet };
