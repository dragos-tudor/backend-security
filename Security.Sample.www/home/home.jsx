import { getContext } from "../scripts/extending.js"

export const Home = (props, elem) =>
  <>
    <h3>{"Welcome " + getContext(elem, "user")?.userName}</h3>
    <p>Security sample web application with cookie and social logins.</p>
    {props.children}
  </>
