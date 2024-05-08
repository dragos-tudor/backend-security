import { createSetUserAction } from "../../frontend-shared/store/actions.js"
import { getUserApi } from "../../frontend-proxy/mod.js"

export const getUser = async (fetchApi, dispatchAction) =>
{
  const [user, error] = await getUserApi(fetchApi)
  if (error) return [, error]

  dispatchAction(createSetUserAction(user))
  return [user]
}