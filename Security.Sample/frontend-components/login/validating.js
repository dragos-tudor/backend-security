const { validateObj, isRequired, hasMaxLength } = await import("/scripts/validating.js")

const userNameValidators = [isRequired, hasMaxLength(10)]
const passwordvalidators = [isRequired, hasMaxLength(10)]

const credentialsValidators = Object.freeze({
  userName: userNameValidators,
  password: passwordvalidators
})

export const validateCredentials = (credentials, validationErrors) =>
  validateObj(credentials, credentialsValidators, validationErrors)

