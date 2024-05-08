const { createStoreState } = await import("/scripts/states.js")

export const AccountState = "account"
export const UserState = "user"

export const createAccountState = (account) => createStoreState(AccountState, account)

export const createUserState = (user) => createStoreState(UserState, user)