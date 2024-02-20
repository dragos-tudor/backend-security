
const clickLogin = (event) => {
  event.preventDefault();
  event.target.parentElement.submit()
}

export const Login = (props) => {
  const queryString = props.queryString
  return (<>
    <h3>Login</h3>
    <div class="login">
      <style css={css}></style>
      <form name="login" method="post" action={"/login" + queryString} >
        <div>
          <label for="user"></label>
          <input type="text" name="user" value="user" placeholder="user name" />
        </div>
        <div>
          <label for="password"></label>
          <input type="password" name="password" value="password" placeholder="password" />
        </div>
        <a href="/" onclick={clickLogin}>Login</a>
      </form>
      <div class="social-logins">
        <a href={"/challenge-google" + queryString}>Connect with Google</a>
        <a href={"/challenge-facebook" + queryString}>Connect with Facebook</a>
        <a href={"/challenge-twitter" + queryString}>Connect with Twitter</a>
      </div>
    </div>
  </>)
}

const css = `
.login {
  display: flex;
  justify-content: center;
  gap: 1rem;
}

.login form>* {
  margin: 0.3rem;
}

.login .social-logins * {
  display: block;
  margin: 0.3rem;
}`