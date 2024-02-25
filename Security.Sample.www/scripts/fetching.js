const addHeader = (headers, name, value)=>{
    if (headers == null) headers = new Headers();
    headers instanceof Headers ? headers.set(name, value) : headers[name] = value;
    return headers;
};
const toJsonBody = (method, obj = {})=>method !== "GET" && method !== "HEAD" ? JSON.stringify(obj) : null;
const createJsonRequest = (method, request)=>({
        ...request,
        body: toJsonBody(method, request.body),
        headers: addHeader(request.headers, "Content-Type", "application/json"),
        method
    });
const createFetchError = async (response)=>Object.freeze({
        name: "FetchError",
        message: await response.text() || response.statusText,
        status: response.status
    });
const createNetworkError = (error)=>Object.freeze({
        name: "NetworkError",
        message: error.message,
        stack: error.stack
    });
const rejectAbortError = (err)=>Promise.reject(err);
const rejectFetchError = async (response)=>Promise.reject(await createFetchError(response));
const rejectNetworkError = (err)=>Promise.reject(createNetworkError(err));
const getJsonResponse = (response)=>response.status !== 204 && response.body != null ? response.json() : Promise.resolve(null);
const getFetchResponse = (response)=>response.ok ? response : rejectFetchError(response);
const fetchData = (url, request, fetchFunc = fetch)=>fetchFunc(url, request).catch((error)=>error.name === "AbortError" ? rejectAbortError(error) : rejectNetworkError(error)).then(getFetchResponse);
const fetchJson = (method)=>async (url, request, fetchFunc = fetch)=>{
        const jsonRequest = createJsonRequest(method, request || {});
        const response = await fetchData(url, jsonRequest, fetchFunc);
        return getJsonResponse(response);
    };
const getJson = fetchJson("GET");
const postJson = fetchJson("POST");
const putJson = fetchJson("PUT");
const deleteJson = fetchJson("DELETE");
const headJson = fetchJson("HEAD");
const patchJson = fetchJson("PATCH");
export { fetchJson as fetchJson, fetchData as fetchData, getJson as getJson, postJson as postJson, putJson as putJson, deleteJson as deleteJson, headJson as headJson, patchJson as patchJson };
const chainMiddlewares = (middlewares, fetchFunc = fetch)=>middlewares?.length ? middlewares.reverse().reduce((acc, next)=>next(acc), fetchFunc) : fetchFunc;
const getSearchParam = (field, value)=>`${encodeURIComponent(field)}=${encodeURIComponent(value)}`;
const getSearchParams = (obj)=>(propName)=>{
        const propValue = obj[propName];
        const isArray = propValue instanceof Array;
        switch(propValue){
            case null:
                return propName;
            case undefined:
                return undefined;
        }
        switch(isArray){
            case false:
                return getSearchParam(propName, propValue);
            case true:
                return propValue.map((value)=>getSearchParam(propName, value)).join("&");
        }
    };
const toStringSearchParams = (obj)=>Object.getOwnPropertyNames(obj).map(getSearchParams(obj)).filter((params)=>!!params).join("&");
const encodeSearchParams = (url, obj = {})=>{
    const searchParams = toStringSearchParams(obj);
    return `${encodeURI(url)}${searchParams ? "?" : ""}${searchParams}`;
};
const absoluteUrlRegex = /^http(s)*:\/\//i;
const toAbsoluteUrl = (url, baseUrl = "")=>absoluteUrlRegex.test(url) ? url : `${baseUrl}${url}`;
export { encodeSearchParams as encodeSearchParams };
export { toAbsoluteUrl as toAbsoluteUrl };
const retry = async (numberOf, func, ...args)=>{
    for(let retries = 0; retries <= numberOf; retries++)try {
        return await func(...args);
    } catch (error) {
        if (retries === numberOf) return Promise.reject(error);
    }
    return Promise.reject("Max retries reached.");
};
const setTokenHeader = (request, token)=>Object.assign(request, {
        headers: request.headers
    }, {
        headers: token
    });
const createBaseUrlMiddleware = (baseUrl)=>(next)=>(url, request)=>next(toAbsoluteUrl(url, baseUrl), request);
const createRetryMiddleware = (retries)=>(next)=>(url, request)=>retry(retries, next, url, request);
const createTokenMiddleware = (token)=>(next)=>(url, request)=>next(url, setTokenHeader(request, token));
export { chainMiddlewares as chainMiddlewares };
export { createBaseUrlMiddleware as createBaseUrlMiddleware, createRetryMiddleware as createRetryMiddleware, createTokenMiddleware as createTokenMiddleware };
