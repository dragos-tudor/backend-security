
const clickLogout = (event) =>{
  event.preventDefault();
  event.target.parentElement.submit();
}

export const Header = (_, elem) => {
  const user = useContext(elem, "user")
  return (
    <div class="header">
      <style css={css} ></style>
      <h4>Security sample</h4>
      <nav>{
        user?
          <ul>
            <li><a href="/index">Home</a></li>
            <li><a href="/account">Account</a></li>
            <li>
              <form name="logout" method="post" action="/api/account/logout?returnUrl=/index" >
                <a href="/" onclick={clickLogout}>Logout</a>
              </form>
            </li>
          </ul>:
          <ul>
            <li><a href="/api/account/login?redirectUrl=/index">Login</a></li>
          </ul>
        }
      </nav>
  </div>
  )
}

const css = `.header {
  display: flex;
  justify-content: space-between;
}

.header ul {
  list-style: none;
}`