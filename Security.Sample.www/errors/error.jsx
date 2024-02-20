
export const Error = (props) => {
  const errorName = props.errorName;
  const errorDescription = props.errorDescription;
  return (
    <>
      <h3>Error</h3>
      <p>An error occurred while processing your request.</p>

      <h4>{errorName}</h4>
      <p>{errorDescription}</p>
    </>
  )
}
