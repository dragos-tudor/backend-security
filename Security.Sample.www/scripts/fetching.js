const setJsonContentTypeHeader = (headers)=>setHeader(headers, "Content-Type", "application/json");
const setHeader = (headers, name, value)=>{
    headers instanceof Headers ? headers.set(name, value) : headers[name] = value;
    return headers;
};
const HttpMethods = Object.freeze({
    GET: "GET",
    POST: "POST",
    PUT: "PUT",
    PATCH: "PATCH",
    DELETE: "DELETE",
    OPTIONS: "OPTIONS",
    HEAD: "HEAD"
});
const isGetMethod = (method)=>method === HttpMethods.GET;
const isHeadMethod = (method)=>method === HttpMethods.HEAD;
const toJsonRequestBody = (obj)=>obj ? JSON.stringify(obj) : null;
const ensureRequestHeaders = (request)=>request.headers = request.headers ?? new Headers();
const ensureRequestMethod = (request, method)=>request.method = request.method ?? method;
const setRequestBody = (request, body)=>body ? Object.assign(request, {
        body
    }) : body;
const allowRequestBody = (method)=>!(isGetMethod(method) || isHeadMethod(method));
const allowRequestSearchParams = (method)=>isGetMethod(method) || isHeadMethod(method);
const existsResponseContent = (response)=>response.status !== 204 && response.body != null;
const isResponseOk = (response)=>response.ok;
const getTextResponse = async (response)=>await response.text() || response.statusText;
const getJsonResponse = (response)=>existsResponseContent(response) ? response.json() : Promise.resolve(null);
const getSearchParam = (field, value)=>`${encodeURIComponent(field)}=${encodeURIComponent(value)}`;
const getSearchParams = (obj)=>(propName)=>{
        const propValue = obj[propName];
        switch(propValue){
            case null:
                return propName;
            case undefined:
                return undefined;
        }
        const isArray = propValue instanceof Array;
        switch(isArray){
            case false:
                return getSearchParam(propName, propValue);
            case true:
                return propValue.map((value)=>getSearchParam(propName, value)).join("&");
        }
    };
const toStringSearchParams = (obj)=>Object.getOwnPropertyNames(obj).map(getSearchParams(obj)).filter((params)=>!!params).join("&");
const encodeSearchParams = (url, obj)=>{
    const searchParams = obj ? toStringSearchParams(obj) : undefined;
    return searchParams ? `${url}?${searchParams}` : url;
};
export { encodeSearchParams as encodeSearchParams };
const createAbortError = (error)=>Object.assign(Error(error.code), {
        type: "AbortError"
    });
const createHttpError = (response, message)=>Object.assign(Error(message), {
        type: "HttpError",
        status: response.status
    });
const createNetworkError = (error)=>Object.assign(Error(error.message), {
        type: "NetworkError",
        stack: error.stack
    });
const fetchData = async (fetch, url, request)=>{
    try {
        const response = await fetch(url, request);
        return isResponseOk(response) ? [
            response
        ] : [
            ,
            createHttpError(response, await getTextResponse(response))
        ];
    } catch (error) {
        return error.name === "AbortError" ? [
            ,
            createAbortError(error)
        ] : [
            ,
            createNetworkError(error)
        ];
    }
};
const fetchJson = async (fetch, url, data, request = {})=>{
    ensureRequestMethod(request, HttpMethods.GET);
    if (allowRequestBody(request.method)) {
        ensureRequestHeaders(request, request.headers);
        setJsonContentTypeHeader(request.headers);
        setRequestBody(request, toJsonRequestBody(data));
    }
    const [result, error] = allowRequestSearchParams(request.method) ? await fetchData(fetch, encodeSearchParams(url, data), request) : await fetchData(fetch, url, request);
    return result ? [
        await getJsonResponse(result)
    ] : [
        ,
        error
    ];
};
export { fetchJson as fetchJson, fetchData as fetchData };
const waitTimeout = (timeout)=>new Promise((resolve)=>{
        const timeoutId = setTimeout(()=>{
            clearTimeout(timeoutId);
            resolve();
        }, timeout);
    });
const isFirstIndex = (index)=>index === 0;
const isLastIndex = (index, intervals)=>index === intervals.length - 1;
const expBackoff = async (intervals, func, ...args)=>{
    for (const [index, interval] of intervals.entries()){
        if (!isFirstIndex(index)) await waitTimeout(interval);
        const result = await func(...args);
        if (!(result instanceof Error)) return result;
        if (isLastIndex(index, intervals)) return result;
    }
};
export { expBackoff as expBackoff };
const retry = async (retries, func, ...args)=>{
    do {
        const result = await func(...args);
        if (!(result instanceof Error)) return result;
        if (!retries) return result;
    }while (retries--)
};
export { retry as retry };
export { HttpMethods as HttpMethods };
