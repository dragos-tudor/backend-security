
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<string?> CallbackOAuthAsync<TOptions> (
    HttpContext context,
    Func<HttpContext, Task<AuthenticateResult>> authenticate,
    Func<HttpContext, ClaimsPrincipal, AuthenticationProperties, AuthenticationTicket> signin)
  where TOptions : OAuthOptions
  {
    var authenticateResult = await authenticate(context);
    if (authenticateResult.Failure is not null) {
      SetResponseRedirect(context.Response, BuildAuthenticationErrorPath(authenticateResult.Failure));
      return string.Empty;
    }

    if (authenticateResult.Principal is not null) {
      signin(context, authenticateResult.Principal, authenticateResult.Properties!);
      SetResponseRedirect(context.Response, authenticateResult.Properties!.RedirectUri!);
      return authenticateResult.Properties!.RedirectUri;
    }

    return string.Empty;
  }

}