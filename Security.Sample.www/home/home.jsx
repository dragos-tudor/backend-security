
export const Home = (props) => {
  const userName = props.userName
  return (<>
    <h3>{"Welcome" + userName}</h3>
    <p>Security sample web application with cookie and social logins.</p>
  </>)
}