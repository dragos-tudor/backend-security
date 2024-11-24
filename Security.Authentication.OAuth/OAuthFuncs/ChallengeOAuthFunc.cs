using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public delegate string? ChallengeOAuthFunc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps) where TOptions : OAuthOptions;
}