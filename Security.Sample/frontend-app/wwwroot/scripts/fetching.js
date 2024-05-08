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
const toJsonRequestBody = (obj)=>JSON.stringify(obj);
const setRequestBody = (request, body)=>request.body = body;
const setRequestHeaders = (request, headers = new Headers())=>request.headers = headers;
const setRequestMethod = (request, method)=>request.method = method;
const setRequestSignal = (request, signal)=>request.signal = signal;
const existsRequestHeaders = (request)=>request.headers;
const existsRequestMethod = (request)=>request.method;
const isObjectRequestBody = (request)=>typeof request.body === "object";
const isGetOrHeadMethod = (method)=>isGetMethod(method) || isHeadMethod(method);
const getHeaderContentLength = (headers)=>getHeaderValue(headers, "content-length");
const getHeaderValue = (headers, name)=>headers.get(name);
const existsResponseBody = (response)=>response.body != null;
const existsResponseContent = (response)=>!isNoContentResponse(response) && !isEmptyContentResponse(response) && existsResponseBody(response);
const isEmptyContentResponse = (response)=>getHeaderContentLength(response.headers) == 0;
const isNoContentResponse = (response)=>response.status === 204;
const isOkResponse = (response)=>response.ok;
const getTextResponse = async (response)=>await response.text() || response.statusText;
const getJsonResponse = (response)=>existsResponseContent(response) ? response.json() : Promise.resolve(null);
const AbortError = "AbortError";
const NetworkError = "NetworkError";
const HttpError = "HttpError";
const isAbortError = (error)=>error?.type === AbortError;
const isHttpError = (error)=>error?.type === HttpError;
const isNetworkError = (error)=>error?.type === NetworkError;
export { AbortError as AbortError };
export { NetworkError as NetworkError };
export { HttpError as HttpError };
export { isAbortError as isAbortError };
export { isHttpError as isHttpError };
export { isNetworkError as isNetworkError };
const createAbortError = (error)=>Object.assign(Error(error.message), {
        type: AbortError
    });
const createHttpError = (response, message)=>Object.assign(Error(message), {
        type: HttpError,
        response
    });
const createNetworkError = (error)=>Object.assign(Error(error.message), {
        type: NetworkError,
        stack: error.stack
    });
const createRequestSignal = (controller, timeoutId)=>Object.freeze({
        signal: controller.signal,
        timeoutId
    });
const getRequestSignal = (timeout)=>{
    const controller = new AbortController();
    const timeoutId = setTimeout(()=>controller.abort(Error("timeout error")), timeout);
    return createRequestSignal(controller, timeoutId);
};
const fetchData = async (fetch, url, request)=>{
    try {
        const response = await fetch(url, request);
        return isOkResponse(response) ? [
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
const fetchJson = async (fetch, url, request = {})=>{
    existsRequestMethod(request) || setRequestMethod(request, HttpMethods.GET);
    existsRequestHeaders(request) || setRequestHeaders(request, request.headers);
    if (!isGetOrHeadMethod(request.method)) {
        setJsonContentTypeHeader(request.headers);
        isObjectRequestBody(request) && setRequestBody(request, toJsonRequestBody(request.body));
    }
    const [result, error] = await fetchData(fetch, url, request);
    return result ? [
        await getJsonResponse(result)
    ] : [
        ,
        error
    ];
};
const fetchWithTimeout = async (fetch, url, request, timeout = 500)=>{
    const { signal , timeoutId  } = getRequestSignal(timeout);
    setRequestSignal(request, signal);
    try {
        return await fetch(url, request);
    } finally{
        clearTimeout(timeoutId);
    }
};
export { fetchJson as fetchJson, fetchData as fetchData, fetchWithTimeout as fetchWithTimeout };
const waitTimeout = (timeout)=>new Promise((resolve)=>{
        const timeoutId = setTimeout(()=>{
            clearTimeout(timeoutId);
            resolve();
        }, timeout);
    });
const isFirstIndex = (index)=>index === 0;
const isLastIndex = (index, intervals)=>index === intervals.length - 1;
const expBackoff = async (intervals, fetch, ...args)=>{
    for (const [index, interval] of intervals.entries()){
        if (!isFirstIndex(index)) await waitTimeout(interval);
        const result = await fetch(...args);
        const failure = result[1];
        if (!failure) return result;
        if (isLastIndex(index, intervals)) return result;
    }
};
export { expBackoff as expBackoff };
const retry = async (retries, fetch, ...args)=>{
    do {
        const result = await fetch(...args);
        const failure = result[1];
        if (!failure) return result;
        if (!retries) return result;
    }while (retries--)
};
export { retry as retry };
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
export { HttpMethods as HttpMethods };
