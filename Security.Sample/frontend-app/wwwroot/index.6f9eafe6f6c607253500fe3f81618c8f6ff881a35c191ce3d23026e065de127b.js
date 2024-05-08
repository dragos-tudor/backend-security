const getLocationSearchParam = (location1, paramName)=>getLocationSearchParams(location1).get(paramName);
const getLocationPathName = (location1)=>location1.pathname;
const getLocationPathNameAndSearch = (location1)=>location1.pathname + (location1.search ?? "");
const getLocationSearchParams = (location1)=>new URLSearchParams(location1.search);
const getLocationUrl = (location1)=>location1.href;
const RoutePaths = Object.freeze({
    root: "/",
    login: "/login",
    home: "/home",
    forbidden: "/forbidden",
    info: "/info"
});
const RedirectParamName = "redirect";
const hasLocationPath = (location1, path)=>getLocationPathName(location1) === path;
const isLoginPath = (location1)=>hasLocationPath(location1, RoutePaths.login);
const isRootPath = (location1)=>hasLocationPath(location1, RoutePaths.root);
const resolveRedirectParamPath = (location1, fallback)=>isRootPath(location1) ? fallback : getLocationPathName(location1);
const setRedirectParam = (location1, fallback)=>RedirectParamName + "=" + encodeURIComponent(resolveRedirectParamPath(location1, fallback));
const getRedirectParam = (location1)=>decodeURIComponent(getLocationSearchParam(location1, RedirectParamName));
const getRedirectedLogin = (location1)=>RoutePaths.login + "?" + setRedirectParam(location1, RoutePaths.home);
const setRequestCredentials = (request, value = "omit")=>Object.assign(request, {
        credentials: value
    });
const setRequestMode = (request, mode = "cors")=>Object.assign(request, {
        mode
    });
const setRequestRedirect = (request, redirect)=>Object.assign(request, {
        redirect
    });
const isForbiddenResponse = (response)=>response?.status === 403;
const isUnauthorizedResponse = (response)=>response?.status === 401;
const { fetchJson } = await import("/scripts/fetching.js");
const getFetchApi = (fetch1, navigate, handleError, location1 = globalThis.location)=>async (url, request = {})=>{
        setRequestMode(request, "cors");
        setRequestRedirect(request, "manual");
        setRequestCredentials(request, "include");
        const [data, error] = await fetchJson(fetch1, url, request);
        if (!error) return [
            data
        ];
        if (error) handleError(error);
        if (isUnauthorizedResponse(error.response)) !isLoginPath(location1) && navigate(getRedirectedLogin(location1));
        if (isForbiddenResponse(error.response)) navigate(RoutePaths.forbidden);
        return [
            ,
            error
        ];
    };
const resolveLocation = (location1)=>location1 ?? globalThis.location;
const LanguageParamName = "lang";
const getLanguageParam = (location1)=>getLocationSearchParam(location1, LanguageParamName);
const Languages = Object.freeze({
    en: "en",
    ro: "ro"
});
const isEnglishLanguage = (lang)=>Languages.en === lang;
const isRomanianLanguage = (lang)=>Languages.ro === lang;
const getLabelsPath = (lang)=>`/scripts/labels.${lang}.js`;
const importLabels = async (lang)=>(await import(getLabelsPath(lang))).default;
const Labels = Object.freeze({
    userName: "User name",
    password: "Password",
    signin: "Signin with credentials",
    signinWithGoogle: "Signin with Google",
    signinWithFacebook: "Signin with Facebook",
    signinWithTwitter: "Signin with Twitter",
    signout: "Sign out",
    userClaims: "User claims",
    schemeName: "Scheme name",
    accessDenied: "Access denied",
    unauthorized: "You are not authorize to access resource.",
    gotoHome: "Goto hone",
    home: "Home",
    login: "Login",
    info: "Info"
});
const resolveLabels = async (lang)=>isEnglishLanguage(lang) ? Labels : await importLabels(lang);
const ServiceNames = Object.freeze({
    apiUrl: "api-url",
    fetchApi: "fetch-api",
    labels: "labels",
    language: "language",
    validationErrors: "validation-errors"
});
const createServices = (apiUrl, fetchApi, labels, language, validationErrors)=>Object.freeze({
        [ServiceNames.apiUrl]: apiUrl,
        [ServiceNames.fetchApi]: fetchApi,
        [ServiceNames.labels]: labels,
        [ServiceNames.language]: language,
        [ServiceNames.validationErrors]: validationErrors
    });
const getValidationErrorsPath = (lang)=>`/scripts/validations.${lang}.js`;
const importValidationErrors = async (lang)=>(await import(getValidationErrorsPath(lang))).default;
const { ValidationErrors } = await import("/scripts/validating.js");
const resolveValidationErrors = async (lang)=>isEnglishLanguage(lang) ? ValidationErrors : await importValidationErrors(lang);
const { createStoreState } = await import("/scripts/states.js");
const AppState = "appState";
const AccountState = "account";
const createAppState = (account, user)=>createStoreState(AppState, {
        account,
        user
    });
const { createReducer } = await import("/scripts/states.js");
const createAppReducer = ()=>createReducer(AppState, {
        setUser: (state, action)=>({
                ...state,
                ...action.payload
            }),
        setAccount: (state, action)=>({
                ...state,
                ...action.payload
            })
    });
const { getService } = await import("/scripts/rendering.js");
const useApiUrl = (elem1)=>getService(elem1, ServiceNames.apiUrl);
const useFetchApi = (elem1, props = {})=>props[ServiceNames.fetchApi] ?? getService(elem1, ServiceNames.fetchApi);
const useLabels = (elem1)=>getService(elem1, ServiceNames.labels);
const useLanguage = (elem1)=>getService(elem1, ServiceNames.language);
const useValidationErrors = (elem1)=>getService(elem1, ServiceNames.validationErrors);
await import("/scripts/rendering.js");
const github = React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    viewBox: "0 0 50 50"
}, React.createElement("path", {
    d: "M25,8c9.389,0,17,7.611,17,17c0,7.548-4.921,13.944-11.729,16.163c0.002-0.006,0.005-0.016,0.005-0.016 s-1.147-0.538-1.123-1.498c0.027-1.056,0-3.521,0-4.426c0-1.553-0.983-2.654-0.983-2.654s7.709,0.087,7.709-8.139 c0-3.174-1.659-4.825-1.659-4.825s0.871-3.388-0.302-4.825c-1.315-0.142-3.669,1.257-4.675,1.91c0,0-1.593-0.653-4.244-0.653 c-2.65,0-4.244,0.653-4.244,0.653c-1.005-0.653-3.36-2.052-4.675-1.91c-1.173,1.437-0.302,4.825-0.302,4.825 s-1.659,1.652-1.659,4.825c0,8.226,7.709,8.139,7.709,8.139s-0.777,0.878-0.946,2.168c-0.538,0.183-1.33,0.408-1.969,0.408 c-1.673,0-2.946-1.626-3.412-2.379c-0.46-0.742-1.403-1.365-2.281-1.365c-0.579,0-0.861,0.289-0.861,0.62 c0,0.331,0.811,0.562,1.347,1.175c1.129,1.294,1.109,4.202,5.132,4.202c0.437,0,1.329-0.107,2-0.198 c-0.004,0.916-0.005,1.882,0.009,2.447c0.024,0.96-1.123,1.498-1.123,1.498s0.003,0.01,0.005,0.016C12.921,38.944,8,32.548,8,25 C8,15.611,15.611,8,25,8z"
}));
React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    viewBox: "0 0 48 48"
}, React.createElement("path", {
    fill: "#4caf50",
    d: "M45,16.2l-5,2.75l-5,4.75L35,40h7c1.657,0,3-1.343,3-3V16.2z"
}), React.createElement("path", {
    fill: "#1e88e5",
    d: "M3,16.2l3.614,1.71L13,23.7V40H6c-1.657,0-3-1.343-3-3V16.2z"
}), React.createElement("polygon", {
    fill: "#e53935",
    points: "35,11.2 24,19.45 13,11.2 12,17 13,23.7 24,31.95 35,23.7 36,17"
}), React.createElement("path", {
    fill: "#c62828",
    d: "M3,12.298V16.2l10,7.5V11.2L9.876,8.859C9.132,8.301,8.228,8,7.298,8h0C4.924,8,3,9.924,3,12.298z"
}), React.createElement("path", {
    fill: "#fbc02d",
    d: "M45,12.298V16.2l-10,7.5V11.2l3.124-2.341C38.868,8.301,39.772,8,40.702,8h0 C43.076,8,45,9.924,45,12.298z"
}));
const google = React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    viewBox: "0 0 48 48"
}, React.createElement("path", {
    fill: "#FFC107",
    d: "M43.611,20.083H42V20H24v8h11.303c-1.649,4.657-6.08,8-11.303,8c-6.627,0-12-5.373-12-12c0-6.627,5.373-12,12-12c3.059,0,5.842,1.154,7.961,3.039l5.657-5.657C34.046,6.053,29.268,4,24,4C12.955,4,4,12.955,4,24c0,11.045,8.955,20,20,20c11.045,0,20-8.955,20-20C44,22.659,43.862,21.35,43.611,20.083z"
}), React.createElement("path", {
    fill: "#FF3D00",
    d: "M6.306,14.691l6.571,4.819C14.655,15.108,18.961,12,24,12c3.059,0,5.842,1.154,7.961,3.039l5.657-5.657C34.046,6.053,29.268,4,24,4C16.318,4,9.656,8.337,6.306,14.691z"
}), React.createElement("path", {
    fill: "#4CAF50",
    d: "M24,44c5.166,0,9.86-1.977,13.409-5.192l-6.19-5.238C29.211,35.091,26.715,36,24,36c-5.202,0-9.619-3.317-11.283-7.946l-6.522,5.025C9.505,39.556,16.227,44,24,44z"
}), React.createElement("path", {
    fill: "#1976D2",
    d: "M43.611,20.083H42V20H24v8h11.303c-0.792,2.237-2.231,4.166-4.087,5.571c0.001-0.001,0.002-0.001,0.003-0.002l6.19,5.238C36.971,39.205,44,34,44,24C44,22.659,43.862,21.35,43.611,20.083z"
}));
const facebook = React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    viewBox: "0 0 48 48"
}, React.createElement("path", {
    fill: "#3f51b5",
    d: "M24 4A20 20 0 1 0 24 44A20 20 0 1 0 24 4Z"
}), React.createElement("path", {
    fill: "#fff",
    d: "M29.368,24H26v12h-5V24h-3v-4h3v-2.41c0.002-3.508,1.459-5.59,5.592-5.59H30v4h-2.287 C26.104,16,26,16.6,26,17.723V20h4L29.368,24z"
}));
const linkedin = React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    viewBox: "0 0 64 64"
}, React.createElement("path", {
    d: "M 23.773438 12 C 12.855437 12 12 12.854437 12 23.773438 L 12 40.226562 C 12 51.144563 12.855438 52 23.773438 52 L 40.226562 52 C 51.144563 52 52 51.145563 52 40.226562 L 52 23.773438 C 52 12.854437 51.145563 12 40.226562 12 L 23.773438 12 z M 21.167969 16 L 42.832031 16 C 47.625031 16 48 16.374969 48 21.167969 L 48 42.832031 C 48 47.625031 47.624031 48 42.832031 48 L 21.167969 48 C 16.374969 48 16 47.624031 16 42.832031 L 16 21.167969 C 16 16.374969 16.374969 16 21.167969 16 z M 22.501953 18.503906 C 20.872953 18.503906 19.552734 19.824172 19.552734 21.451172 C 19.552734 23.078172 20.871953 24.400391 22.501953 24.400391 C 24.126953 24.400391 25.447266 23.079172 25.447266 21.451172 C 25.447266 19.826172 24.126953 18.503906 22.501953 18.503906 z M 37.933594 26.322266 C 35.473594 26.322266 33.823437 27.672172 33.148438 28.951172 L 33.078125 28.951172 L 33.078125 26.728516 L 28.228516 26.728516 L 28.228516 43 L 33.28125 43 L 33.28125 34.949219 C 33.28125 32.826219 33.687359 30.771484 36.318359 30.771484 C 38.912359 30.771484 38.945312 33.200891 38.945312 35.087891 L 38.945312 43 L 44 43 L 44 34.074219 C 44 29.692219 43.054594 26.322266 37.933594 26.322266 z M 19.972656 26.728516 L 19.972656 43 L 25.029297 43 L 25.029297 26.728516 L 19.972656 26.728516 z"
}));
const twitter = React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    viewBox: "0 0 48 48"
}, React.createElement("path", {
    fill: "#03a9f4",
    d: "M24 4A20 20 0 1 0 24 44A20 20 0 1 0 24 4Z"
}), React.createElement("path", {
    fill: "#fff",
    d: "M36,17.12c-0.882,0.391-1.999,0.758-3,0.88c1.018-0.604,2.633-1.862,3-3 c-0.951,0.559-2.671,1.156-3.793,1.372C31.311,15.422,30.033,15,28.617,15C25.897,15,24,17.305,24,20v2c-4,0-7.9-3.047-10.327-6 c-0.427,0.721-0.667,1.565-0.667,2.457c0,1.819,1.671,3.665,2.994,4.543c-0.807-0.025-2.335-0.641-3-1c0,0.016,0,0.036,0,0.057 c0,2.367,1.661,3.974,3.912,4.422C16.501,26.592,16,27,14.072,27c0.626,1.935,3.773,2.958,5.928,3c-1.686,1.307-4.692,2-7,2 c-0.399,0-0.615,0.022-1-0.023C14.178,33.357,17.22,34,20,34c9.057,0,14-6.918,14-13.37c0-0.212-0.007-0.922-0.018-1.13 C34.95,18.818,35.342,18.104,36,17.12"
}));
const spinner = React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    viewBox: "0 0 100 100"
}, React.createElement("path", {
    fill: "#fff",
    d: "M73,50c0-12.7-10.3-23-23-23S27,37.3,27,50 M30.9,50c0-10.5,8.5-19.1,19.1-19.1S69.1,39.5,69.1,50"
}, React.createElement("animateTransform", {
    xmlns: "http://www.w3.org/2000/svg",
    attributeName: "transform",
    attributeType: "XML",
    type: "rotate",
    dur: "1s",
    from: "0 50 50",
    to: "360 50 50",
    repeatCount: "indefinite"
})));
const youtube = React.createElement("svg", {
    xmlns: "http://www.w3.org/2000/svg",
    viewBox: "0 0 64 64"
}, React.createElement("path", {
    d: "M 32 15 C 14.938 15 12.659656 15.177734 10.472656 17.427734 C 8.2856563 19.677734 8 23.252 8 32 C 8 40.748 8.2856562 44.323266 10.472656 46.572266 C 12.659656 48.821266 14.938 49 32 49 C 49.062 49 51.340344 48.821266 53.527344 46.572266 C 55.714344 44.322266 56 40.748 56 32 C 56 23.252 55.714344 19.677734 53.527344 17.427734 C 51.340344 15.177734 49.062 15 32 15 z M 32 19 C 45.969 19 49.379156 19.062422 50.535156 20.232422 C 51.691156 21.402422 52 24.538 52 32 C 52 39.462 51.691156 42.597578 50.535156 43.767578 C 49.379156 44.937578 45.969 45 32 45 C 18.031 45 14.620844 44.937578 13.464844 43.767578 C 12.308844 42.597578 12.03125 39.462 12.03125 32 C 12.03125 24.538 12.308844 21.402422 13.464844 20.232422 C 14.620844 19.062422 18.031 19 32 19 z M 27.949219 25.017578 L 27.949219 38.982422 L 40.095703 31.945312 L 27.949219 25.017578 z"
}));
const rendering = await import("/scripts/rendering.js");
const routing = await import("/scripts/routing.js");
const states = await import("/scripts/states.js");
const { setEffects, setStates, update } = rendering;
const { getStoreStates, setSelectors } = states;
const dispatchAction = (elem1)=>(action)=>states.dispatchAction(elem1, action);
const navigate = (elem1)=>(url)=>routing.navigate(elem1, url);
const setInitialEffect = (elem1, name, func)=>rendering.setInitialEffect(setEffects(elem1), name, func);
const useEffect = (elem1, name, func, deps)=>rendering.useEffect(setEffects(elem1), name, func, deps);
const usePostEffect = (elem1, name, func, deps)=>rendering.useEffect(setEffects(elem1), name, async ()=>(await Promise.resolve(), func()), deps);
const useSelector = (elem1, name, func)=>states.useSelector(setSelectors(elem1), name, func, getStoreStates(elem1));
const useState = (elem1, name, value, deps)=>{
    const [state, setState] = rendering.useState(setStates(elem1), name, value, deps);
    return [
        state,
        (value)=>{
            const result = setState(value);
            update(elem1);
            return result;
        }
    ];
};
const createLocationSearchParam = (name, value)=>name + "=" + encodeURIComponent(value);
const getPathWithEnglishParam = (location1)=>getLocationPathName(location1) + "?" + createLocationSearchParam(LanguageParamName, Languages.en);
const getPathWithRomanianParam = (location1)=>getLocationPathName(location1) + "?" + createLocationSearchParam(LanguageParamName, Languages.ro);
const { useLocation } = await import("/scripts/routing.js");
const Language = (_, elem1)=>{
    const lang = useLanguage(elem1);
    const location1 = useLocation(elem1);
    return React.createElement(React.Fragment, null, React.createElement("a", {
        href: location1 && getPathWithEnglishParam(location1),
        hidden: isEnglishLanguage(lang),
        target: "_self"
    }, Languages.en), React.createElement("a", {
        href: location1 && getPathWithRomanianParam(location1),
        hidden: isRomanianLanguage(lang),
        target: "_self"
    }, Languages.ro));
};
const selectIsAuthenticated = (states)=>states[AppState][AccountState].isAuthenticated;
const { NavLink } = await import("/scripts/routing.js");
const NavLinks = (props, elem1)=>{
    const isAuthenticated = useSelector(elem1, "is-authenticated", selectIsAuthenticated);
    const labels = useLabels(elem1);
    return React.createElement(React.Fragment, null, React.createElement("style", {
        css: css
    }), isAuthenticated ? React.createElement("nav", null, React.createElement(NavLink, {
        class: "navlinks-link",
        href: RoutePaths.home
    }, labels["home"]), React.createElement(NavLink, {
        class: "navlinks-link",
        href: RoutePaths.info
    }, labels["info"])) : React.createElement("nav", null, React.createElement(NavLink, {
        class: "navlinks-link",
        href: RoutePaths.login
    }, labels["login"])));
};
const css = `
.navlinks-link {
  margin-left: 1rem
}`;
const { createAction } = await import("/scripts/states.js");
const createSetUserAction = (user)=>createAction(`${AppState}/setUser`, {
        user
    });
const createIsAuthenticatedAction = (isAuthenticated)=>createAction(`${AppState}/setAccount`, {
        isAuthenticated
    });
const { HttpMethods } = await import("/scripts/fetching.js");
const signInAccountApi = (credentials, fetchApi)=>fetchApi("/accounts/signin", {
        method: HttpMethods.POST,
        body: credentials
    });
const { HttpMethods: HttpMethods1 } = await import("/scripts/fetching.js");
const signOutAccoutApi = (fetchApi)=>fetchApi("/accounts/signout", {
        method: HttpMethods1.POST
    });
const isAuthenticatedAccountApi = (fetchApi)=>fetchApi("/accounts/authenticated", {});
const signOutAccount = async (fetchApi, dispatchAction, navigate)=>{
    const [, error] = await signOutAccoutApi(fetchApi);
    if (error) return [
        ,
        error
    ];
    dispatchAction(createSetUserAction(null));
    navigate(RoutePaths.login);
    return [
        true
    ];
};
const Logout = (props, elem1)=>{
    const fetchApi = useFetchApi(elem1, props);
    const labels = useLabels(elem1);
    return React.createElement(React.Fragment, null, React.createElement("a", {
        onclick: ()=>signOutAccount(fetchApi, dispatchAction(elem1), navigate(elem1))
    }, labels["signout"]));
};
const Header = (props, elem1)=>{
    const isAuthenticated = useSelector(elem1, "is-authenticated", selectIsAuthenticated);
    return React.createElement(React.Fragment, null, React.createElement("style", {
        css: css1
    }), React.createElement("h2", null, React.createElement("a", {
        href: "/",
        class: "header-logo"
    }, "security sample")), React.createElement(Language, {
        class: "header-language"
    }), React.createElement(NavLinks, {
        class: "header-navlinks"
    }), isAuthenticated && React.createElement(Logout, null));
};
const css1 = `
header {
  display: flex;
  align-items: end;
  gap: 2rem;
  padding: 2rem;
  background-color: var(--neutral-dark-color);
}

.header-logo {
  font-family: var(--ff-serif);
  color: var(--label-color);
  text-transform: uppercase;
}

.header-language {
  flex: 1 1;
  text-align: right;
}`;
const Footer = ()=>React.createElement(React.Fragment, null, React.createElement("style", {
        css: css2
    }), React.createElement("section", {
        class: "footer social-icons"
    }, React.createElement("a", {
        class: "footer-social-icon",
        href: "https://github.com/dragos-tudor",
        target: "_blank"
    }, github), React.createElement("a", {
        class: "footer-social-icon",
        href: "https://linkedin.com/in/dragos-tudor-marian",
        target: "_blank"
    }, linkedin), React.createElement("a", {
        class: "footer-social-icon",
        href: "https://youtube.com/@dragos-tudor",
        target: "_blank"
    }, youtube)), React.createElement("div", {
        class: "footer-me"
    }, "dragos.tudor - 2024"));
const css2 = `
footer {
  padding-block: 2rem 3rem;
  text-align: center;
  background-color: var(--neutral-dark-color);
}

.footer-social-icon {
  margin-inline: 0.5rem 0.5rem;
  font-size: 4rem;
}

.footer-me {
  margin-top: 1rem;
}`;
const getErrorDescription = (location1)=>getLocationSearchParams(location1).get("description");
const { NavLink: NavLink1 } = await import("/scripts/routing.js");
const Forbidden = (props)=>{
    const labels = useLabels(elem);
    const location1 = resolveLocation(props.location);
    const error = getErrorDescription(location1);
    return React.createElement(React.Fragment, null, React.createElement("style", {
        css: css3
    }), React.createElement("h3", null, labels["accessDenied"]), React.createElement("p", null, labels["unauthorized"]), React.createElement("p", null, error), React.createElement(NavLink1, {
        href: "/home"
    }, labels["gotoHome"]));
};
const css3 = `
forbidden {
  display: block;
  margin: 3rem;
}

forbidden h3 {
  color: var(--error-color)
}`;
const loadHome = async ()=>{
    const { Home } = await import("/home.4963dd9df79adddcb11229bb64ca305c65ecea3c20f96d422d92752dedf81eb3.js");
    return React.createElement(Home, null);
};
const createCredentials = (userName, password)=>Object.freeze({
        userName,
        password
    });
const getHtmlDescendant = (elem1, name)=>elem1.querySelector(name.toLowerCase());
const getHtmlButton = (elem1)=>getHtmlDescendant(elem1, "button");
const hasLocationRedirect = (location1)=>getLocationUrl(location1).includes(RedirectParamName);
const signInAccount = async (credentials, location1, fetchApi, dispatchAction, navigate)=>{
    const [, error] = await signInAccountApi(credentials, fetchApi);
    if (error) return [
        ,
        error
    ];
    dispatchAction(createIsAuthenticatedAction(true));
    hasLocationRedirect(location1) ? navigate(getRedirectParam(location1)) : navigate(RoutePaths.home);
    return [
        true
    ];
};
const { validateObj, isRequired, hasMaxLength } = await import("/scripts/validating.js");
const userNameValidators = [
    isRequired,
    hasMaxLength(10)
];
const passwordvalidators = [
    isRequired,
    hasMaxLength(10)
];
const credentialsValidators = Object.freeze({
    userName: userNameValidators,
    password: passwordvalidators
});
const validateCredentials = (credentials, validationErrors)=>validateObj(credentials, credentialsValidators, validationErrors);
const Login = (props, elem1)=>{
    const apiUrl = useApiUrl(elem1);
    const fetchApi = useFetchApi(elem1, props);
    const labels = useLabels(elem1);
    const validationErrors = useValidationErrors(elem1);
    const location1 = resolveLocation(props.location);
    const currentUrl = getLocationUrl(location1);
    const returnUrl = encodeURIComponent(currentUrl);
    const [userName, setUserName] = useState(elem1, "userName", null, []);
    const [password, setPassword] = useState(elem1, "password", null, []);
    const [signing, setSigning] = useState(elem1, "signing", false, []);
    const credentials = createCredentials(userName, password);
    const validationResult = validateCredentials(credentials, validationErrors);
    const validCredentials = validationResult.isValid;
    const userNameError = userName == null || !validationResult.userName;
    const passwordError = password == null || !validationResult.password;
    return React.createElement(React.Fragment, null, React.createElement("style", {
        css: css4
    }), React.createElement("section", {
        class: "local-authentication"
    }, React.createElement("label", {
        for: "userName"
    }, labels["userName"]), React.createElement("input", {
        id: "userName",
        type: "text",
        onchange: ({ target })=>setUserName(target.value),
        placeholder: labels["userName"]
    }), React.createElement("label", {
        for: "password"
    }, labels["password"]), React.createElement("input", {
        id: "password",
        type: "password",
        onchange: ({ target })=>setPassword(target.value),
        onblur: ()=>getHtmlButton(elem1).focus(),
        placeholder: labels["password"]
    }), React.createElement("button", {
        class: "signing",
        disabled: !validCredentials || signing,
        onclick: async ()=>{
            setSigning(true);
            await signInAccount(credentials, location1, fetchApi, dispatchAction(elem1), navigate(elem1));
            setSigning(false);
        }
    }, React.createElement("span", {
        hidden: !signing
    }, spinner), React.createElement("span", null, labels["signin"])), React.createElement("label", {
        hidden: userNameError
    }, labels["userName"]), React.createElement("span", {
        hidden: userNameError,
        class: "error"
    }, validationResult.userName), React.createElement("label", {
        hidden: passwordError
    }, labels["password"]), React.createElement("span", {
        hidden: passwordError,
        class: "error"
    }, validationResult.password)), React.createElement("div", {
        class: "or"
    }, "or"), React.createElement("section", {
        class: "remote-authentication"
    }, React.createElement("a", {
        class: "auth-provider",
        href: `${apiUrl}/accounts/challenge-google?returnUrl=${returnUrl}`
    }, google, React.createElement("span", {
        class: "auth-provider-label"
    }, labels["signinWithGoogle"])), React.createElement("a", {
        class: "auth-provider",
        href: `${apiUrl}/accounts/challenge-facebook?returnUrl=${returnUrl}`
    }, facebook, React.createElement("span", {
        class: "auth-provider-label"
    }, labels["signinWithFacebook"])), React.createElement("a", {
        class: "auth-provider",
        href: `${apiUrl}/accounts/challenge-twitter?returnUrl=${returnUrl}`
    }, twitter, React.createElement("span", {
        class: "auth-provider-label"
    }, labels["signinWithTwitter"]))));
};
const css4 = `
login {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  gap: 2rem;
  height: 100%;
}

@media (max-width: 40rem) {
  login {
    flex-direction: column;
  }
}


.local-authentication,
.remote-authentication {
  padding: 1em;
  border: thick solid var(--neutral-dark-color);
}

.local-authentication {
  display: grid;
  grid-template-columns: auto 1fr;
  justify-items: end;
  align-items: center;
  column-gap: 1rem;
  row-gap: 1rem;
}

.local-authentication .signing {
  grid-column: 1 / span 2;
}

.local-authentication .error {
  color: var(--error-color)
}

.or {
  color: var(--neutral--light-color);
}

.remote-authentication {
  display: grid;
  row-gap: 1rem;
}

.remote-authentication .auth-provider .auth-provider-label {
  margin-left: 0.5rem;
}`;
const Info = (_, elem1)=>{
    const labels = useLabels(elem1);
    return React.createElement(React.Fragment, null, React.createElement("style", {
        css: css5
    }), React.createElement("h3", null, labels["info"]), React.createElement("span", null, "Dragos Tudor sofware developer."));
};
const css5 = `
info {
  display: block;
  margin: 3rem;
}`;
const { Route } = await import("/scripts/routing.js");
const Routes = ()=>React.createElement(React.Fragment, null, React.createElement(Route, {
        path: RoutePaths.home,
        load: loadHome
    }), React.createElement(Route, {
        path: RoutePaths.login,
        child: React.createElement(Login, null),
        index: true
    }), React.createElement(Route, {
        path: RoutePaths.info,
        child: React.createElement(Info, null)
    }), React.createElement(Route, {
        path: RoutePaths.forbidden,
        child: React.createElement(Forbidden, null)
    }));
const isAuthenticationSuccedded = (isAuthenticated, error)=>!error && isAuthenticated;
const startApp = async (fetchApi, dispatchAction, navigate, location1 = globalThis.location)=>{
    const [isAuthenticated, error] = await isAuthenticatedAccountApi(fetchApi);
    if (!isAuthenticationSuccedded(isAuthenticated, error)) isLoginPath(location1) ? navigate(getLocationPathNameAndSearch(location1)) : navigate(getRedirectedLogin(location1));
    if (error) return [
        ,
        error
    ];
    if (!isAuthenticated) return [
        isAuthenticated
    ];
    dispatchAction(createIsAuthenticatedAction(isAuthenticated));
    isLoginPath(location1) || isRootPath(location1) ? navigate(RoutePaths.home) : navigate(getLocationPathName(location1));
    return [
        isAuthenticated
    ];
};
const { Router } = await import("/scripts/routing.js");
const Application = (props, elem1)=>{
    const fetchApi = useFetchApi(elem1, props);
    const location1 = resolveLocation(props.location);
    const [starting, setStarting] = useState(elem1, "starting", true, []);
    usePostEffect(elem1, "start-app", async ()=>{
        await startApp(fetchApi, dispatchAction(elem1), navigate(elem1), location1);
        setStarting(false);
    }, []);
    return React.createElement(React.Fragment, null, React.createElement("style", {
        css: css6
    }), React.createElement(Router, {
        "no-skip": true
    }, React.createElement(Header, null), React.createElement("main", null, React.createElement("div", {
        hidden: !starting,
        class: "app-spinner"
    }, spinner), React.createElement(Routes, null)), React.createElement(Footer, null)));
};
const css6 = `
application {
  height: 100vh;
}

router {
  display: grid;
  grid-template-rows: auto 1fr auto;
  height: inherit;
}

main {
  height: 100%;
  justify-self: center;
  align-self: center;
}

routes, route, suspense {
  display: block;
  height: inherit;
}

main {
  display: grid;
  justify-items: center;
  align-items: center;
}

.app-spinner svg  {
  height: 5rem;
}`;
const getMessage = (props)=>props.message;
const getTimeout = (props)=>props.timeout ?? 3000;
const hideHtmlElement = (elem1)=>(elem1.style.display = "none", elem1);
const showHtmlElement = (elem1, display = "block")=>(elem1.style.display = display, elem1);
const toggleError = (elem1, timeout)=>showHtmlElement(elem1) && setTimeout(()=>hideHtmlElement(elem1), timeout);
const Error = (props, elem1)=>{
    const [timeout] = useState(elem1, "timeout", getTimeout(props), []);
    const message = getMessage(props);
    if (message) useEffect(elem1, "toggle-error", ()=>{
        const timeoutId = toggleError(elem1, timeout);
        setInitialEffect(elem1, "toggle-error", ()=>clearTimeout(timeoutId));
    });
    return React.createElement(React.Fragment, null, React.createElement("style", {
        css: css7
    }), React.createElement("p", null, message));
};
const css7 = `
error {
  display: none;
  position: absolute;
  transition: display 1s ease-in-out;
  bottom: 5rem;
  left: 2rem;
  padding: 1rem;
  border: thin solid var(--error-color);
  color: var(--info-color);
  background-color: var(--neutral-light-color);
}`;
const sanitizeMessage = (message)=>message.replaceAll("\"", "");
const { update: update1 } = await import("/scripts/rendering.js");
const updateError = (elem1, message)=>update1(elem1, React.createElement(Error, {
        message: sanitizeMessage(message)
    }));
const { Router: Router1 } = await import("/scripts/routing.js");
const getErrorElement = (elem1)=>getHtmlDescendant(elem1, Error.name);
const getRouterElement = (elem1)=>getHtmlDescendant(elem1, Router1.name);
const { fetchWithTimeout } = await import("/scripts/fetching.js");
const { navigate: navigate1 } = await import("/scripts/routing.js");
const { Services } = await import("/scripts/rendering.js");
const { Store } = await import("/scripts/states.js");
const language = getLanguageParam(location) ?? Languages.en;
const labels = await resolveLabels(language);
const validationErrors = await resolveValidationErrors(language);
const Root = (props, elem1)=>{
    const { apiUrl, apiTimeout, location: location1 } = props;
    const fetchApi = getFetchApi((url, request)=>fetchWithTimeout(fetch, apiUrl + url, request, apiTimeout), (url)=>navigate1(getRouterElement(elem1), url), (error)=>updateError(getErrorElement(elem1), error.message), resolveLocation(location1));
    const services = createServices(apiUrl, fetchApi, labels, language, validationErrors);
    return React.createElement(React.Fragment, null, React.createElement("style", {
        css: css8
    }), React.createElement(Store, {
        state: createAppState({
            isAuthenticated: false
        }),
        reducer: createAppReducer()
    }), React.createElement(Services, services), React.createElement(Application, null), React.createElement(Error, null));
};
const css8 = `
a {
  text-decoration: none;
  outline: none;
  cursor: pointer;
  transition: color var(--transition-interval) ease-in-out;
  border: 0;
  color: var(--accent-color);
}

a:hover, a:focus {
  color: var(--accent-dark-color);
}

a * {
  vertical-align: middle;
}

button {
  padding: 1rem;
  font-size: var(--font-size);
  outline: 0;
  cursor: pointer;
  transition: color var(--transition-interval) ease-in-out, border-color var(--transition-interval);
  border: thin solid var(--accent-color);
  color: var(--accent-color);
  background-color: var(--neutral-dark-color);
}

button:hover, button:focus {
  color: var(--accent-dark-color);
  border-color: var(--accent-dark-color);
}

button:disabled {
  cursor: default;
  color: var(--info-color);
  border-color: var(--neutral--light-color);
}

label {
  cursor: pointer;
  opacity: 75%;
  color: var(--label-color);
}

input {
  padding: 1rem;
  font-size: var(--font-size);
  outline: none;
  transition: border-color var(--transition-interval) ease-in-out;
  border: thin solid var(--neutral-dark-color);
  background-color: var(--neutral-dark-color);
  color: var(--info-color);
}

input:hover, input:focus {
  border-color: var(--info-color);
}

svg {
  height: 1em;
}`;
export { Root as Root };
