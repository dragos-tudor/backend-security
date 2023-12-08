
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class Funcs {

  public static bool IsOAuthCallbackPath (HttpContext context, OAuthOptions authOptions) =>
    context.Request.Path == authOptions.CallbackPath;

}