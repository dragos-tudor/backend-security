import { google, facebook, twitter } from "../images/icons.jsx"
import { useState } from "../scripts/extending.js"
const { HttpMethods } = await import("/scripts/fetching.js")
const { getService, update } = await import("/scripts/rendering.js")
const { navigate } = await import("/scripts/routing.js")


const loginUser = async (credentials, apiFetch, elem) => {
  const user = await apiFetch("/account/login", credentials, { method: HttpMethods.POST })
  return user && navigate(elem, "/home")
}

const updateState = (setState, elem) => (event) => {
  setState(event.target.value)
  return update(elem)
}

export const Login = (props, elem) =>
{
  const apiFetch = getService(elem, "api-fetch")
  const [userName, setUserName] = useState(elem, "userName", "", [])
  const [password, setPassword] = useState(elem, "password", "", [])

  return <>
    <style css={css}></style>
    <signin-form>
      <div>
        <label for="userName">User name</label>
        <input id="userName" type="text" placeholder="user name here" tabindex="1"/>
      </div>
      <div>
        <label for="password">Password</label>
        <input id="password" type="password" placeholder="password here" tabindex="2"/>
      </div>
      <div>
        <button tabindex="4">Signin with credentials</button>
      </div>
    </signin-form>
    <or>or</or>
    <external-auth>
      <a class="auth-provider" tabindex="5">
        {google}
        <span>Signin with Google</span>
      </a>
      <a class="auth-provider" tabindex="6">
        {facebook}
        <span>Signin with Facebook</span>
      </a>
      <a class="auth-provider" tabindex="7">
        {twitter}
        <span>Signin with Twitter</span>
      </a>
    </external-auth>
  </>
}

const css = `
login {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  height: 100%;
}

signin-form {
  display: grid;
  justify-items: end;
  row-gap: 0.5em;
}

signin-form, external-auth {
  padding: 1em;
  border-radius: var(--default-radius);
  border: 3px solid var(--dark-primary-color);
}

or {
  margin: 1em;
  color: var(--light-primary-color);
}

external-auth .auth-provider {
  display: block;
}

@media screen and (max-width: 800px) {
  login {
    flex-direction: column;
  }
}`