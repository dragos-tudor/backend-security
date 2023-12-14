using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public delegate string? ChallengeFunc<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties)
  where TOptions: OAuthOptions;
}