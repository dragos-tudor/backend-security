import { useState } from "../extensions/states.js"
const { HttpMethods } = await import("../scripts/fetching.js")
const { getService, update } = await import("../scripts/rendering.js")
const { navigate } = await import("../scripts/routing.js")


const loginUser = async (credentials, apiFetch, elem) => {
  const user = await apiFetch("/account/login", credentials, { method: HttpMethods.POST })
  return user && navigate(elem, "/home")
}

const updateState = (setState, elem) => (event) => {
  setState(event.target.value)
  return update(elem)
}

export const Login = ({returnUrl}, elem) =>
{
  const apiFetch = getService(elem, "api-fetch")
  const [userName, setUserName] = useState(elem, "userName", "", [])
  const [password, setPassword] = useState(elem, "password", "", [])
  const [confirmPassword, setConfirmPassword] = useState(elem, "confirmPassword", "", [])

  return <>
    <h3>Login</h3>
    <style css={css}></style>
    <div class="login">
      <div class="cookie-login">`
        <div>
          <label for="userName"></label>
          <input type="text" name="userName" value={userName} onchange={updateState(setUserName, elem)} placeholder="user name" />
        </div>
        <div>
          <label for="password"></label>
          <input type="password" name="password" value={password} onchange={updateState(setPassword, elem)} placeholder="password" />
        </div>
        <div>
          <label for="confirmPassword"></label>
          <input type="password" name="confirmPassword" value={confirmPassword} onchange={updateState(setConfirmPassword, elem)} placeholder="confirm password" />
        </div>
        <div class="error"></div>
        <a href="/" onclick={() => loginUser({userName, password, confirmPassword}, apiJson)}>Login</a>
      </div>
      <div class="social-logins">
        <a href={"/challenge-google" + returnUrl}>Connect with Google</a>
        <a href={"/challenge-facebook" + returnUrl}>Connect with Facebook</a>
        <a href={"/challenge-twitter" + returnUrl}>Connect with Twitter</a>
      </div>
    </div>
  </>
}

const css = `
.login {
  display: flex;
  justify-content: center;
  gap: 1rem;
}

.login .cookie-login>* {
  margin: 0.3rem;
}

.login .social-logins * {
  display: block;
  margin: 0.3rem;
}`