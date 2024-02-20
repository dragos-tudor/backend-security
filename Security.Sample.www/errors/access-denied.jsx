
export const AccessDenied = (props) => {
  const errorDescription = props.errorDescription
  return (<>
    <h3>Access denied</h3>
    <p>You are not authorize to access resource.</p>
    <p>{errorDescription}</p>
  </>)
}