const ServiceNames = Object.freeze({
    apiUrl: "api-url",
    fetchApi: "fetch-api",
    labels: "labels",
    language: "language",
    validationErrors: "validation-errors"
});
const { getService } = await import("/scripts/rendering.js");
const useFetchApi = (elem, props = {})=>props[ServiceNames.fetchApi] ?? getService(elem, ServiceNames.fetchApi);
const useLabels = (elem)=>getService(elem, ServiceNames.labels);
const { createStoreState } = await import("/scripts/states.js");
const AccountState = "account";
const UserState = "user";
const selectIsAuthenticated = (states)=>states[AccountState].isAuthenticated;
const rendering = await import("/scripts/rendering.js");
await import("/scripts/routing.js");
const states = await import("/scripts/states.js");
const { setEffects, setStates, update } = rendering;
const { getStoreStates, setSelectors } = states;
const dispatchAction = (elem)=>(action)=>states.dispatchAction(elem, action);
const useEffect = (elem, name, func, deps)=>rendering.useEffect(setEffects(elem), name, func, deps);
const useSelector = (elem, name, func)=>states.useSelector(setSelectors(elem), name, func, getStoreStates(elem));
const useState = (elem, name, value, deps)=>{
    const [state, setState] = rendering.useState(setStates(elem), name, value, deps);
    return [
        state,
        (value)=>{
            const result = setState(value);
            update(elem);
            return result;
        }
    ];
};
const concatClaim = (result, claim)=>result + (result ? ", " : "") + claim;
const { createAction } = await import("/scripts/states.js");
const createSetUserAction = (user)=>createAction(`${UserState}/setUser`, {
        user
    });
const { HttpMethods } = await import("/scripts/fetching.js");
const { HttpMethods: HttpMethods1 } = await import("/scripts/fetching.js");
const getUserApi = (fetchApi)=>fetchApi("/users", {});
const getUser = async (fetchApi, dispatchAction)=>{
    const [user, error] = await getUserApi(fetchApi);
    if (error) return [
        ,
        error
    ];
    dispatchAction(createSetUserAction(user));
    return [
        user
    ];
};
const Home = (props, elem)=>{
    const fetchApi = useFetchApi(elem, props);
    const labels = useLabels(elem);
    const [user, setUser] = useState(elem, "user", null, []);
    const isAuthenticated = useSelector(elem, "is-authenticated", selectIsAuthenticated);
    useEffect(elem, "get-user", async ()=>{
        const [user] = await getUser(fetchApi, dispatchAction(elem));
        user && setUser(user);
    }, [
        isAuthenticated
    ]);
    return React.createElement(React.Fragment, null, React.createElement("style", {
        css: css
    }), React.createElement("section", null, React.createElement("div", {
        class: "home-user-detail"
    }, React.createElement("label", null, labels["userName"] + ": "), React.createElement("span", null, user?.userName)), React.createElement("div", {
        class: "home-user-detail"
    }, React.createElement("label", null, labels["schemeName"] + ": "), React.createElement("span", null, user?.schemeName)), React.createElement("div", {
        class: "home-user-detail"
    }, React.createElement("label", null, labels["userClaims"] + ": "), React.createElement("span", null, user?.userClaims?.reduce(concatClaim, "")))), props.children);
};
const css = `
home {
  display: block;
  margin: 3rem;
}

.home-user-detail {
  color: var(--info-color)
}`;
export { Home as Home };
