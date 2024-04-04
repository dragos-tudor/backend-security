using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Security.Authentication.AuthenticationFuncs;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  static AuthenticationProperties GetAuthenticationProperties(AuthenticateResult authResult) =>
    authResult.Properties ?? CreateAuthenticationProperties();

  public static AuthenticateResult GetDefaultAuthenticateResult(HttpContext context) =>
   IsAuthenticatedPrincipal(context.User)?
    AuthenticateResult.Success(CreateAuthenticationTicket(context.User, "context.User")):
    AuthenticateResult.NoResult();
}