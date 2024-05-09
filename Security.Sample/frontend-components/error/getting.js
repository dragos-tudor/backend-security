
const defaultTimeout = 3000

export const getErrorMessage = (error) => error?.message ?? error

export const getPropsMessage = (props) => props.message

export const getPropsTimeout = (props) => props.timeout ?? defaultTimeout
