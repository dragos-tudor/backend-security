
export const AccessDeniedError = ({description}) =>
  <>
    <h3>Access denied</h3>
    <p>You are not authorize to access resource.</p>
    <p>{description}</p>
  </>