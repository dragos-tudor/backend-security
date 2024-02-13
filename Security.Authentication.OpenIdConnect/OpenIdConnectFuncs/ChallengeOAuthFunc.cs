using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public delegate string? ChallengeOidcFunc<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties)
  where TOptions: OpenIdConnectOptions;
}